Create Table Country(Id int identity(1,1), Name nvarchar(100), IsoCode char(3))
Create Table Breed(Id int identity(1,1), Name nvarchar(40))
Create Table Pet(Id int identity(1,1), PetOwnerId int, Name nvarchar(40), DateOfBirth date)

