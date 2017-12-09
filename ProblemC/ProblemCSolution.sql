--Proplem C
--Given Query

create table PetOwner
(
	Id		int identity(1,1)
,	Name		nvarchar(200)
,	PolicyNumber	varchar(40)
,	PolicyDate	datetime
,	CountryId	int
)
go


create table Pet
(
	Id		int identity(1,1)
,	PetOwnerId	int
,	Name		nvarchar(40)
,	DateOfBirth	date
)
go

create table Breed
(
	Id		int identity(1,1)
,	Name		nvarchar(40)	
)
go

create table Country
(
	Id		int identity(1,1)
,	Name		nvarchar(100)
,	IsoCode	char(3)
)
go

/*
Question1 . 
a) a.	Given whatever you design, write a query that is as performant as you can make it that shows how many pets were enrolled on the day Mount St. Helens erupted.
Answer.
 Some Assumption: 
1)PetOwner Will have one policy Number under which his all pets enrolled.
2)Policy Should have atleast one pet.
//Last eruption	July 10, 2008
https://en.wikipedia.org/wiki/Mount_St._Helens
Change in database
Need to add index for policydate in petOwner table
*/
CREATE NONCLUSTERED INDEX [petOwner_PolicyDate] ON [dbo].[PetOwner]
([PolicyDate] ASC )WITH (PAD_INDEX = OFF,
 STATISTICS_NORECOMPUTE = OFF,
 SORT_IN_TEMPDB = OFF,
 DROP_EXISTING = OFF,
 ONLINE = OFF,
 ALLOW_ROW_LOCKS = ON,
 ALLOW_PAGE_LOCKS = ON)
GO
--Need index for petownerID in pet table

CREATE NONCLUSTERED INDEX [pet_PetOwnerID_DateOfBirth] ON [dbo].[Pet]
([PetOwnerId] ASC, [DateOfBirth] ASC)WITH (PAD_INDEX = OFF,
 STATISTICS_NORECOMPUTE = OFF,
 SORT_IN_TEMPDB = OFF,
 DROP_EXISTING = OFF,
 ONLINE = OFF,
 ALLOW_ROW_LOCKS = ON,
 ALLOW_PAGE_LOCKS = ON)
GO

--Query to get the count of Pets on 10th July 2008(policy bought on day Mount St. Helens erupted).
SELECT COUNT(*) AS [PetCount] FROM [dbo].[petOwner] AS po JOIN [dbo].[Pet] AS p ON po.[PolicyDate]='07-10-2008' and p.PetOwnerId =po.id

/*
b)Given whatever you design, optimize your system for sending birthday cards to pet owners for their pets’ birthdays.
Answer
Change in table 
Need to add Email address to the pet owner table 
*/
CREATE TABLE [dbo].[PetOwner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[PolicyNumber] [varchar](40) NOT NULL,
	[PolicyDate] [datetime] NULL,
	[CountryId] [int] NULL,
	[Email] [varchar](320) NULL,
 CONSTRAINT [PK_PetOwner] PRIMARY KEY CLUSTERED 
([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]

--Query to get Email id. 

SELECT po.[Email],
po.[Name] As OwnerName,
p.[Name] As PetName
 From [dbo].[PetOwner] AS po JOIN [dbo].[Pet] AS p ON DATEPART(d, p.[DateOfBirth]) = DATEPART(d, GETDATE())
    AND DATEPART(m, p.[DateOfBirth]) = DATEPART(m, GETDATE()) AND po.Id = p.[PetOwnerId]
-- I have Joined [dbo].[PetOwner] and [dbo].[Pet] to get birthdate. I need to compare DateOfBirth of Pet with todays date. So Comparing Date and Month.


--c)c.	Given whatever you design, create a stored procedure that transfers all pets from one owner to another owner.  
--If an owner has multiple pets, do them one at a time.  
--If transfer fails during operation, the data should not be left in an inconsistent or invalid state.
--i.	Do you like the stipulation of doing one at a time?  Yes/no why?
--Answer
--Change in database : Adding INDEX for faster querying the result
--Adding index for PetOwner in pet table
CREATE NONCLUSTERED INDEX [pet_PetOwnerID] ON [dbo].[Pet]
([PetOwnerId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

--Stored procedure to transfer the Pet from One owner to other

CREATE PROCEDURE [dbo].[sp_TransferPet]
	@OldOwnerId int,
	@NewOwnerId int
AS
BEGIN
BEGIN TRY
BEGIN TRAN TransferPet
	WHILE EXISTS (SELECT 1 FROM  [dbo].[Pet] WHERE petownerid=@OldOwnerId)
	BEGIN
		UPDATE TOP(1) [dbo].[Pet] 
		SET PetOwnerId= @NewOwnerId
		WHERE PetOwnerId=@OldOwnerId
	END
	COMMIT TRAN TransferPet
END TRY	
	BEGIN CATCH
	ROLLBACK TRAN TransferPet
	END CATCH
END

--I am using Transaction to roll back if something goes wrong during transfer

--2)
--Do you like the stipulation of doing one at a time?  Yes/no why?
Answer:
--NO, As we need maintain Transcation (ACID property), Sql server can take care of this as SQL server Query engine optimizes the query.

--d) Given whatever you design, create the most performant implementation you can that allows for finding pets by breed name.  This design can be useless for all other scenarios and impractical as long as it meets the given criteria
Answer:
-- De-Normalizing Table to have Breed details into the same table to avoid join. and adding clustered index on breed and non-clustered on id
CREATE TABLE [dbo].[PetForBreedSearch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PetOwnerId] [int] NULL,
	[Name] [nvarchar](40) NULL,
	[DateOfBirth] [date] NULL,
	[Breed] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_PetForBreedSearch_1] PRIMARY KEY NONCLUSTERED 
([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]

CREATE CLUSTERED INDEX [CI_Breed] ON [dbo].[PetForBreedSearch]
([Breed] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
--Answer:
SELECT * FROM [PetForBreedSearch] WHERE Breed = 'SomeBreedName'
--e) Create a stored procedure that generates policy numbers for the pet owner upon enrollment.  The format for the policy number should be [COUNTRYCODE][AUTOINCREMENT] where [COUNTRYCODE] is the 3 character ISO country code and [AUTOINCREMENT] is an incrementing number that starts from zero.  The policy number must be 13 characters long at all times.  It should not be possible to hand out a duplicate policy number given two pet owners enrolling at the same time.  Answer the following questions
--Answer:
CREATE PROCEDURE [dbo].[GetPolicyNumber]
	@OwnerID INT,
	@PolicyId CHAR(13) OUTPUT
AS
BEGIN
DECLARE  @isocode INT;
SELECT @isocode = c.IsoCode FROM [dbo].[PetOwner] AS po JOIN [dbo].[Country] AS c ON po.countryid = c.Id
SELECT @isocode+REPLICATE('0',10-LEN(RTRIM(@PolicyId))) + RTRIM(@PolicyId)
END
/*
i.	Do you believe your policy number algorithm is scalable under heavy new enrollment load?
Answer : Yes if its in trigger. Logic will work.
ii.	Is the best place for this code inside of an SQL stored procedure or somewhere else?
Anser : Trigger Should be the best place for this logic.
*/