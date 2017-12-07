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
Name NVARCHAR(200),
PolicyNumber VARCHAR(40),
PolicyDate DATETIME,
CountryId INT NOT NULL,
CONSTRAINT [FK_CountryId_PetOwner] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country](Id),
PRIMARY KEY CLUSTERED ([Id] ASC))


IF exists (select * from sys.objects where object_id = object_id(N'[dbo].[Pet]') and type in (N'U'))
    drop table [dbo].[Pet]
GO

CREATE Table [dbo].[Pet](
Id int identity(1,1),
PetOwnerId INT NOT NULL,
Name nvarchar(40),
DateOfBirth DATE NOT NULL,
CONSTRAINT [FK_PetOwnerId_Pet] FOREIGN KEY ([PetOwnerId]) REFERENCES [dbo].[PetOwner](Id),
PRIMARY KEY CLUSTERED ([Id] ASC))
GO

IF exists (select * from sys.objects where object_id = object_id(N'[dbo].[Breed]') and type in (N'U'))
    drop table [dbo].[Breed]
GO

CREATE Table [dbo].[Breed](
Id int identity(1,1),
Name nvarchar(40),
PRIMARY KEY CLUSTERED ([Id] ASC))
GO

CREATE FUNCTION [dbo].[GetNextPolicyNumber]
  ( @ISOCode CHAR(3), @PolicyId INT )
RETURNS TABLE
AS
RETURN
  (
  SELECT @ISOCode+REPLICATE('0',10-LEN(RTRIM(@PolicyId))) + RTRIM(@PolicyId)  AS PolicyNumber 
  )
  GO
