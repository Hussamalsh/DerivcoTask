CREATE PROCEDURE [dbo].[spStudent_GetById]
	@id as uniqueidentifier
AS
begin
	SELECT * 
	from dbo.Student
	where Id = @id;
end;


