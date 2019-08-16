CREATE PROCEDURE [dbo].[spStudent_DeleteById]
	@id as uniqueidentifier
AS
begin
	delete from dbo.Student
	where [Id] = @id;
end;

