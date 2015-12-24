/*
IF EXISTS ( SELECT Id FROM sysObjects WHERE Name like  'InsertPetOwner' ) 
BEGIN           
    DROP procedure InsertPetOwner
END   
*/

CREATE PROCEDURE [dbo].[InsertPetOwner]
    @Name nvarchar(200), 
    @IsoCode char(3),
	@Email nvarchar(256)  
AS

declare @countryId int
declare @policyNumber varchar(40)
declare @policyDate datetime

select @countryId = Id from dbo.Country where IsoCode = @IsoCode
select @policyNumber = dbo.generatePolicyNumber(@IsoCode)
select @policyDate = getdate()

INSERT INTO PetOwner(Name, PolicyNumber, PolicyDate, CountryId, Email)
VALUES (@Name, @policyNumber, @policyDate , @CountryId, @Email);

select CAST(@@identity AS int ) 'Id', @Name 'Name', @policyNumber 'PolicyNumber', @policyDate 'PolicyDate', @IsoCode 'CountryIsoCode', @Email 'Email'

