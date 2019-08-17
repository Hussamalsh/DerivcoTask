using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.DataLib.Persistence
{
    public class DapperHelper
    {
        private IDbConnection db;
        private string connString = @"Data Source = (LocalDb)\MSSQLLocalDB;Initial Catalog =StudentDB; Integrated Security = SSPI;";

        public DapperHelper()
        {
            this.db = new SqlConnection(connString);
        }

        public List<Data.Models.Student> ExecuteProcedure_GetAllStudent()
        {
            //return this.db.Query<Data.Models.Student>("Select * from student").ToList();
            return this.db.Query<Data.Models.Student>("spStudent_GetAll", commandType: CommandType.StoredProcedure).ToList();
        }

        public Data.Models.Student ExecuteProcedure_GetById(Guid id)
        {
            //return this.db.Query<Data.Models.Student>("SELECT * from dbo.Student where Id = @id",new {id} ).SingleOrDefault();
            return this.db.Query<Data.Models.Student>("spStudent_GetById", new { id },commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        public void ExecuteProcedure_AddStudent(Data.Models.Student student)
        {
            /*var sql = "INSERT INTO [dbo].[Student] ([Id],[FirstName],[LastName],[Email],[Address],[City],[Zip],[Phone])" +
                "VALUES(@id, @firstName, @lastName, @email, @address, @city, @zip, @phone)";
            this.db.Execute(sql, student);*/
            this.db.Execute("spStudent_AddStudent", student, commandType: CommandType.StoredProcedure);
        }

        public void ExecuteProcedure_UpdateStudent(Data.Models.Student student)
        {
            /*var sql = "UPDATE [dbo].[Student] " +
                      "set [FirstName] = @firstName, " +
                      "    [LastName] = @lastName, " +
                      "	   [Email] = @email, " +
                      "	   [Address] = @address, " +
                      "	   [City] = @city, " +
                      "    [Zip] = @zip," +
                      "	   [Phone] = @phone" +
                      "where [Id] = @id";
            this.db.Execute(sql,student);*/
            this.db.Execute("spStudent_UpdateStudent", student, commandType: CommandType.StoredProcedure);
        }

        public void ExecuteProcedure_DeleteById(Guid id)
        {
            //this.db.Execute("delete from dbo.Student where [Id] = @id;",new {id});
            this.db.Execute("spStudent_DeleteById", new { id }, commandType: CommandType.StoredProcedure);
        }
    }
}
