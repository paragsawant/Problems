INSERT INTO [dbo].[Country] Values('United State','USA')
INSERT INTO [dbo].[Country] Values('Australia','AUS')
INSERT INTO [dbo].[Country] Values('India','IND')
INSERT INTO [dbo].[Country] Values('Japan','JPN')
INSERT INTO [dbo].[Country] Values('United Kingdom','GBR')
INSERT INTO [dbo].[Country] Values('Brazil','BRA')
INSERT INTO [dbo].[Country] Values('Spain','ESP')
INSERT INTO [dbo].[Country] Values('Canada','CAN')
INSERT INTO [dbo].[Country] Values('Belgium','BEL')
INSERT INTO [dbo].[Country] Values('China','CHN')
INSERT INTO [dbo].[Country] Values('France','FRA')
INSERT INTO [dbo].[Country] Values('Germany','DEU')

INSERT INTO [dbo].[Breed] Values('German Shepherd')
INSERT INTO [dbo].[Breed] Values('Labrador Retriever')
INSERT INTO [dbo].[Breed] Values('Rottweiler')
INSERT INTO [dbo].[Breed] Values('Bull Dog')
INSERT INTO [dbo].[Breed] Values('Beagle')
INSERT INTO [dbo].[Breed] Values('British Shorthair')
INSERT INTO [dbo].[Breed] Values('Siamese Cat')
INSERT INTO [dbo].[Breed] Values('Persian Cat')
INSERT INTO [dbo].[Breed] Values('Ragdoll')
INSERT INTO [dbo].[Breed] Values('Maine Coon')

INSERT INTO [dbo].[PetOwner] VALUES('Parag','USA0000000001',GETDATE(),1)
INSERT INTO [dbo].[PetOwner] VALUES('Jack','USA0000000002',GETDATE(),2)
INSERT INTO [dbo].[PetOwner] VALUES('Amanda','USA0000000003',GETDATE(),5)

INSERT INTO [dbo].[Pet] Values(1,'dog',Getdate())
INSERT INTO [dbo].[Pet] Values(1,'cat',Getdate())
INSERT INTO [dbo].[Pet] Values(2,'dog',Getdate())
INSERT INTO [dbo].[Pet] Values(3,'cat',Getdate())
INSERT INTO [dbo].[Pet] Values(3,'dog',Getdate())
INSERT INTO [dbo].[Pet] Values(2,'cat',Getdate())