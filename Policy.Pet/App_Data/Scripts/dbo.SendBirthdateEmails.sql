CREATE PROCEDURE [dbo].[SendBirthdatEmails]
	@date datetime
AS

	declare @email varchar(100)

	select @email = po.Email
	from Pet p
	join PetOwner po
	on p.PetOwnerId = po.Id
	where p.DateOfBirth = @date

	/*
	  Send email
	*/
