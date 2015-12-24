
create PROCEDURE [dbo].[TransferOwner]
	@fromOwnerId int,
	@toOwnerId int
AS

	DECLARE pet_cursor CURSOR FOR 
	select Id
	from Pet
	where PetOwnerId = @fromOwnerId


	declare @petId int

	OPEN pet_cursor

	FETCH NEXT FROM pet_cursor 
	INTO @petId

	WHILE @@FETCH_STATUS = 0
	BEGIN
			
		update Pet
		set PetOwnerId = @toOwnerId
		where Id = @petId

		IF (@@ROWCOUNT > 0)
		BEGIN
			 select 'Success!'
		END
		ELSE 
		BEGIN
			select 'Not success!'
		END

			-- Get the next pet.
		FETCH NEXT FROM pet_cursor 
		INTO @petId
	END 

	CLOSE pet_cursor;
	DEALLOCATE pet_cursor;