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

CREATE PROCEDURE [dbo].[EnrollPolicy]
AS
BEGIN
SELECT GETDATE()
END
GO

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[CancelPolicy]') AND TYPE IN (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CancelPolicy]
GO

CREATE PROCEDURE [dbo].[CancelPolicy]
AS
BEGIN
SELECT GETDATE()
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
AS
BEGIN
SELECT GETDATE()
END
GO

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[GetPolicies]') AND TYPE IN (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetPolicies]
GO

CREATE PROCEDURE [dbo].[GetPolicies]
AS
BEGIN
SELECT GETDATE()
END
GO
