CREATE PROCEDURE dbo.EnrollPolicy
    @OwnerName nvarchar(200),
	@countryCode char(3)
AS

declare @countryCode varchar(3)

select @countryCode = c.IsoCode
from PetOwner po
join Country c
on po.CountryId = c.Id


INSERT INTO Policy(PolicyNumber, PolicyDate, PetOwnerId)
VALUES (dbo.generatePolicyNumber(@countryCode), GETDATE(), @PetOwnerId);