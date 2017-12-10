IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[GetCountries]') AND TYPE IN (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetCountries]
GO

CREATE PROCEDURE [dbo].[GetCountries]
AS
BEGIN
       SELECT [Id]
      ,[Name]
      ,[IsoCode]
  FROM [PetInsuranceDb].[dbo].[Country]
END
GO


IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[GetBreeds]') AND TYPE IN (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBreeds]
GO

CREATE PROCEDURE [dbo].[GetBreeds]
AS
BEGIN
       SELECT [Id]
      ,[Name]
  FROM [PetInsuranceDb].[dbo].[Breed]
END
GO


IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[EnrollPolicy]') AND TYPE IN (N'P', N'PC'))
    DROP PROCEDURE [dbo].[EnrollPolicy]
GO
--create procedure [dbo].[CreateOrUpdateTypeDefinition]
CREATE PROCEDURE [dbo].[EnrollPolicy]
	@petOwnerName nvarchar(MAX),
    @countryId int,
    @petDetails [PetTuple] READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRAN
	INSERT INTO [dbo].[PetOwner]
			   ([Name]
			   ,[PolicyNumber]
			   ,[PolicyDate]
			   ,[CountryId])
		 VALUES
			   (@petOwnerName,null,GetDate(),@countryId)
			   DECLARE @latestId int
			   SET @latestId = SCOPE_IDENTITY()
	INSERT INTO [dbo].[Pet]
			   ([PetOwnerId]
			   ,[Name]
			   ,[DateOfBirth]
			   ,[PetType])
     SELECT @latestId,[Item1],convert(datetime,[Item2]),[Item3] FROM @petDetails
	COMMIT TRAN
	Select 1 AS 'result'
END TRY
BEGIN CATCH
		Select -1 AS 'result'
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE() AS ErrorMessage; 
END CATCH
END
GO

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[CancelPolicy]') AND TYPE IN (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CancelPolicy]
GO

CREATE PROCEDURE [dbo].[CancelPolicy]
@policyNumber nvarchar(MAX)
AS
BEGIN
	DECLARE @policyId int

	BEGIN TRY
		BEGIN TRAN
			SELECT @policyId = Id FROM [dbo].[PetOwner] WHere PolicyNumber =@policyNumber
			DELETE FROM [dbo].[Pet] WHERE PetOwnerId = @policyId
			DELETE FROM [dbo].[PetOwner] WHere PolicyNumber =@policyNumber
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE() AS ErrorMessage; 
	END CATCH
END
GO

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[UpdatePolicy]') AND TYPE IN (N'P', N'PC'))
    DROP PROCEDURE [dbo].[UpdatePolicy]
GO

CREATE PROCEDURE [dbo].[UpdatePolicy]
AS
BEGIN
SELECT GETDATE()
END
GO


IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[RemovePetFromPolicy]') AND TYPE IN (N'P', N'PC'))
    DROP PROCEDURE [dbo].[RemovePetFromPolicy]
GO

CREATE PROCEDURE [dbo].[RemovePetFromPolicy]
@petId int,
@petOwnerId int
AS
BEGIN
DECLARE @countOfPet int
SELECT @countOfPet = COUNT(*) FROM [dbo].[Pet] Where PetOwnerId = @petOwnerId
IF @countOfPet > 1
BEGIN
	DELETE FROM [dbo].[Pet] Where Id = @petId and PetOwnerId = @petOwnerId
	SELECT 1 AS 'result'
END
ELSE
BEGIN
	DELETE FROM [dbo].[Pet] Where Id = @petId and PetOwnerId = @petOwnerId
	DELETE FROM [dbo].[PetOwner] WHere id = @petOwnerId
	SELECT 2 AS 'result'
END
END
GO

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[GetPolicies]') AND TYPE IN (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetPolicies]
GO

CREATE PROCEDURE [dbo].[GetPolicies]
@policyNumber nvarchar(MAX) = NULL
AS
BEGIN
IF @policyNumber IS NULL
BEGIN
Select DISTINCT po.Id AS PolicyId,
po.PolicyDate,po.PolicyNumber,po.Name AS [PetOwnerName],po.countryid
 FROM [dbo].[PetOwner] AS po INNER JOIN [dbo].[Pet] AS p ON po.Id = p.PetOwnerId

 Select DISTINCT p.Id AS [PetId],
p.Name AS PetName,p.PetType,p.DateOfBirth, po.Id AS [PetOwnerId]
 FROM [dbo].[PetOwner] AS po INNER JOIN [dbo].[Pet] AS p ON po.Id = p.PetOwnerId
 END
 ELSE
 BEGIN
 
Select DISTINCT po.Id AS PolicyId,
po.PolicyDate,po.PolicyNumber,po.Name AS [PetOwnerName],po.countryid
 FROM [dbo].[PetOwner] AS po INNER JOIN [dbo].[Pet] AS p ON po.Id = p.PetOwnerId WHERE po.PolicyNumber = @policyNumber

 Select DISTINCT p.Id AS [PetId],
p.Name AS PetName,p.PetType,p.DateOfBirth, po.Id AS [PetOwnerId]
 FROM [dbo].[PetOwner] AS po INNER JOIN [dbo].[Pet] AS p ON po.Id = p.PetOwnerId WHERE po.PolicyNumber = @policyNumber

 END
END
GO
