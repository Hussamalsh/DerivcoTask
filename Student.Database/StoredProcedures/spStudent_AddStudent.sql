CREATE PROCEDURE [dbo].[spStudent_AddStudent]
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

	INSERT INTO [dbo].[Student]
			   ([Id]
			   ,[FirstName]
			   ,[LastName]
			   ,[Email]
			   ,[Address]
			   ,[City]
			   ,[Zip]
			   ,[Phone])
	       VALUES
           (@id
           ,@firstName
           ,@lastName
           ,@email
           ,@address
           ,@city
           ,@zip
           ,@phone);
end;
