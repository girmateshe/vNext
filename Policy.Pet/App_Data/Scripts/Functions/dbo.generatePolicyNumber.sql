/*
IF EXISTS ( SELECT Id FROM sysObjects WHERE Name like  'generatePolicyNumber' ) 
BEGIN           
    DROP function generatePolicyNumber
END   
*/
create function generatePolicyNumber(@countrycode varchar(3))
RETURNS VARCHAR(13)
as 
begin
 
  DECLARE @Number int = 1;
  SELECT @number = Id FROM PetOwner

  if(@Number is null) 
  begin
	select @Number = 1
  end

  RETURN left(@countrycode + '_00000000'+ CONVERT(VARCHAR, @Number), 13)

end