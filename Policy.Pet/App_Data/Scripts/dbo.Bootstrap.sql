   
    IF EXISTS ( SELECT Id FROM sysObjects WHERE Name like  'Pet' ) 
    BEGIN           
        DROP TABLE Pet     
    END     

	IF EXISTS ( SELECT Id FROM sysObjects WHERE Name like  'Breed' ) 
    BEGIN           
        DROP TABLE Breed     
    END    
	
	IF EXISTS ( SELECT Id FROM sysObjects WHERE Name like  'PetOwner' ) 
    BEGIN           
        DROP TABLE PetOwner     
    END  

	IF EXISTS ( SELECT Id FROM sysObjects WHERE Name like  'Country' ) 
    BEGIN           
        DROP TABLE Country     
    END  

	create table Breed
	(
		   Id            int identity(1,1) primary key
	,      Name          nvarchar(40) 
	)
	go

	create table Country
	(
		   Id            int identity(1,1) primary key
	,      Name          nvarchar(100)
	,      IsoCode       char(3)
	)
	go

	create table PetOwner
	(
		   Id            int identity(1,1) primary key
	,      Name          nvarchar(200)
	,      PolicyNumber  varchar(40)
	,      PolicyDate    datetime
	,      CountryId     int references Country(Id)
	,	   Email		 varchar(256)
	)
	go

	create table Pet
	(
		   Id            int identity(1,1) primary key
	,      PetOwnerId    int references PetOwner(Id)
	,      Name          nvarchar(40)
	,      DateOfBirth   date
	,      BreadId	     int  references Breed(Id)
	)
	go
	
	insert into Country(Name, IsoCode)
	values('Ethiopia', 'ETH')

	insert into Country(Name, IsoCode)
	values('United States Of America', 'USA')

	insert into Breed(Name)
	values('German Shepherd')

	insert into Breed(Name)
	values('Maltipoo')

	insert into Breed(Name)
	values('Maine Coon')

	select * from Country

	select * from Breed

	DECLARE	@return_value Int

	EXEC	@return_value = [dbo].[InsertPetOwner]
			@Name = N'Dere',
			@IsoCode = N'ETH',
			@Email = N'test@gmail.com'

	SELECT	@return_value as 'Return Value'

	GO

	select * from PetOwner

	
