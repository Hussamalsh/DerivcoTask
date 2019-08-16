CREATE PROCEDURE [dbo].[spStudent_UpdateStudent]
	@id as uniqueidentifier,
	@firstName as varchar(50),
	@lastName as varchar(50),
	@email as varchar(200),
	@address as varchar(200), 
	@city as varchar(50), 
	@zip as varchar(50),
	@phone as varchar(50)
AS
begin
    UPDATE [dbo].[Student]
	set [FirstName] = @firstName,
	    [LastName] = @lastName, 
		[Email] = @email, 
		[Address] = @address, 
		[City] = @city, 
	    [Zip] = @zip, 
		[Phone] = @phone
    where [Id] = @id;
end;
