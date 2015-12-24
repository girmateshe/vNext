
create PROCEDURE [dbo].[FindPetsByBreedName]
	@name nvarchar(40)
AS

select p.*
from Breed b 
join Pet p on b.Id = p.BreedId
where b.Name = @name

	