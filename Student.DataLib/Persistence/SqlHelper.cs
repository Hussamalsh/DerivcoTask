using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Student.Data2.Persistence
{
    public class SqlHelper
    {
        //private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Database=StudentDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
        private string connString2 = @"Data Source = (LocalDb)\MSSQLLocalDB;Initial Catalog =StudentDB; Integrated Security = SSPI;";
        public List<Data.Models.Student> ExecuteProcedure_GetAllStudent()
        {
            List<Data.Models.Student> studentList = new List<Data.Models.Student>();
            using (var sqlConnection = new SqlConnection(connString2))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "spStudent_GetAll";
                    sqlConnection.Open();
                    using (var dataReader = command.ExecuteReader())
                    {
                        Data.Models.Student student;
                        while (dataReader.Read())
                        {
                            student = new Data.Models.Student();
                            student.ID = (Guid)dataReader["Id"];//new Guid((string)dataReader["Id"]);
                            student.FirstName = Convert.ToString(dataReader["FirstName"]);
                            student.LastName = Convert.ToString(dataReader["LastName"]);
                            student.Email = Convert.ToString(dataReader["Email"]);
                            student.Address = Convert.ToString(dataReader["Address"]);
                            student.City = Convert.ToString(dataReader["City"]);
                            student.ZIP = Convert.ToString(dataReader["Zip"]);
                            student.Phone = Convert.ToString(dataReader["Phone"]);
                            //add student to the list
                            studentList.Add(student);
                        }
                    }
                }
            }
            return studentList;
        }


        public Data.Models.Student ExecuteProcedure_GetById(Guid id)
        {
            Data.Models.Student student = null;
            using (var sqlConnection = new SqlConnection(connString2))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "spStudent_GetById";
                    //command.Parameters["@Id"].Value = id;
                    command.Parameters.AddWithValue("@id", id);

                    sqlConnection.Open();
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            student = new Data.Models.Student();
                            student.ID = (Guid)dataReader["Id"];
                            student.FirstName = Convert.ToString(dataReader["FirstName"]);
                            student.LastName = Convert.ToString(dataReader["LastName"]);
                            student.Email = Convert.ToString(dataReader["Email"]);
                            student.Address = Convert.ToString(dataReader["Address"]);
                            student.City = Convert.ToString(dataReader["City"]);
                            student.ZIP = Convert.ToString(dataReader["Zip"]);
                            student.Phone = Convert.ToString(dataReader["Phone"]);
                        }
                    }
                }
            }
            return student;
        }

        public void ExecuteProcedure_AddStudent(Data.Models.Student student)
        {
            using (var sqlConnection = new SqlConnection(connString2))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "spStudent_AddStudent";

                    command.Parameters.AddWithValue("@id", student.ID);
                    command.Parameters.AddWithValue("@firstname", student.FirstName);
                    command.Parameters.AddWithValue("@lastname", student.LastName);
                    command.Parameters.AddWithValue("@email", student.Email);
                    command.Parameters.AddWithValue("@address", student.Address);
                    command.Parameters.AddWithValue("@city", student.City);
                    command.Parameters.AddWithValue("@zip", student.ZIP);
                    command.Parameters.AddWithValue("@phone", student.Phone);
                    sqlConnection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
