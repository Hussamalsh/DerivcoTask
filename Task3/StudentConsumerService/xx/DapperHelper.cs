using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    /*
    public class DapperHelper
    {
        private IDbConnection db;
        private string connString = @"Data Source = (LocalDb)\MSSQLLocalDB;Initial Catalog =StudentDB;User Id=hussam;Password=123456;";

        public DapperHelper()
        {
            this.db = new SqlConnection(connString);
        }

        public List<Student> ExecuteProcedure_GetAllStudent()
        {
            return this.db.Query<Student>("spStudent_GetAll", commandType: CommandType.StoredProcedure).ToList();
        }

        public Student ExecuteProcedure_GetById(Guid id)
        {
            return this.db.Query<Student>("spStudent_GetById", new { id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        public void ExecuteProcedure_AddStudent(Student student)
        {
            this.db.Execute("spStudent_AddStudent", student, commandType: CommandType.StoredProcedure);
        }

        public void ExecuteProcedure_UpdateStudent(Student student)
        {
            this.db.Execute("spStudent_UpdateStudent", student, commandType: CommandType.StoredProcedure);
        }

        public void ExecuteProcedure_DeleteById(Guid id)
        {
            this.db.Execute("spStudent_DeleteById", new { id }, commandType: CommandType.StoredProcedure);
        }
    }*/
}
