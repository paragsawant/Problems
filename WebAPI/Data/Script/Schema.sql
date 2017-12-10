IF NOT EXISTS(SELECT 1 FROM SYS.TYPES WHERE NAME = 'PetTuple' AND IS_TABLE_TYPE = 1 AND SCHEMA_ID('dbo') = SCHEMA_ID)
CREATE TYPE [dbo].[PetTuple] AS TABLE
(
    [ITEM1]	NVARCHAR(MAX),
    [ITEM2]	NVARCHAR(MAX),
    [ITEM3] NVARCHAR(MAX)
)
GO

IF exists (select * from sys.objects where object_id = object_id(N'[dbo].[Country]') and type in (N'U'))
    drop table [dbo].[Country]
GO

CREATE Table [dbo].[Country](
Id int identity(1,1), 
Name nvarchar(100), 
IsoCode char(3),
PRIMARY KEY CLUSTERED ([Id] ASC))

IF exists (select * from sys.objects where object_id = object_id(N'[dbo].[PetOwner]') and type in (N'U'))
    drop table [dbo].[PetOwner]
GO

CREATE Table [dbo].[PetOwner](
Id INT identity(1,1),
Name NVARCHAR(200) NOT NULL,
PolicyNumber VARCHAR(40) NULL,
PolicyDate DATETIME NOT NULL,
CountryId INT NOT NULL,
CONSTRAINT [FK_CountryId_PetOwner] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country](Id),
PRIMARY KEY CLUSTERED ([Id] ASC))


IF exists (select * from sys.objects where object_id = object_id(N'[dbo].[Pet]') and type in (N'U'))
    drop table [dbo].[Pet]
GO

CREATE Table [dbo].[Pet](
Id INT identity(1,1),
PetOwnerId INT NOT NULL,
Name nvarchar(150) NOT NULL,
DateOfBirth DATE NOT NULL,
PetType nvarchar(100) NOT NULL,
CONSTRAINT [FK_PetOwnerId_Pet] FOREIGN KEY ([PetOwnerId]) REFERENCES [dbo].[PetOwner](Id),
PRIMARY KEY CLUSTERED ([Id] ASC))
GO

IF exists (select * from sys.objects where object_id = object_id(N'[dbo].[Breed]') and type in (N'U'))
    drop table [dbo].[Breed]
GO

CREATE Table [dbo].[Breed](
Id int identity(1,1),
PetType nvarchar(100) NOT NULL,
Name nvarchar(40),
PRIMARY KEY CLUSTERED ([Id] ASC))
GO

IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'[dbo].[AutoIncrement_PolicyNumber]' AND [type] = 'TR')
    DROP TRIGGER [dbo].[AutoIncrement_PolicyNumber]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[AutoIncrement_PolicyNumber] ON [dbo].[PetOwner]
    INSTEAD OF INSERT
  AS
  BEGIN
  begin try
  DECLARE @country char(3)
  DECLARE @id int
  DECLARE @policyNumber varchar(MAX)
  DECLARE @countryId int 
  DECLARE @policyDate date
  DECLARE @name VARCHAR(MAX)
  SELECT @countryId = CountryId, @name = name, @policyDate = PolicyDate  from inserted
  select @country = IsoCode FROM [dbo].[Country] Where [Id] = @countryId
  SELECT @id = MAX(id) +1 FROM [dbo].[PetOwner]
  SET @policyNumber =  @country+REPLICATE('0',10-LEN(RTRIM(@id))) + RTRIM(@id)

  INSERT INTO [dbo].[PetOwner]
           ([Name]
           ,[PolicyNumber]
           ,[PolicyDate]
           ,[CountryId])
     VALUES
           (@name,
		   @policyNumber,
		   @policyDate,
		   @countryId)
end try
begin catch
SELECT ERROR_MESSAGE() AS ErrorMessage; 
select -1 as 'result'
end catch
   
  END

GO
