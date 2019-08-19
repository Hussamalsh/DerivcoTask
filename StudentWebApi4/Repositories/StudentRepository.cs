using StudentWebApi4.Common;
using StudentWebApi4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentWebApi4.Repositories
{
    public class StudentRepository
    {
        private TableManager TableManagerObj;
        public StudentRepository()
        {
            TableManagerObj = new TableManager("student");  // 'student' is the name of the table  
        }

        public void addUpdateStudent(string id, Student StudentObj)
        {
            // Insert  
            if (string.IsNullOrEmpty(id))
            {
                StudentObj.PartitionKey = "Student";
                StudentObj.RowKey = Guid.NewGuid().ToString();

                TableManagerObj.InsertEntity<Student>(StudentObj, true);
            }
            // Update  
            else
            {
                StudentObj.PartitionKey = "Student";
                StudentObj.RowKey = id;

                TableManagerObj.InsertEntity<Student>(StudentObj, false);
            }
        }

        public List<Student> getallStudents()
        {
            List<Student> SutdentListObj = TableManagerObj.RetrieveEntity<Student>();
            return SutdentListObj;
        }

        public Student getById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                // Get particular student info                                                   
                List<Student> SutdentListObj = TableManagerObj.RetrieveEntity<Student>("RowKey eq '" + id + "'"); // pass query where RowKey eq value of id
                Student StudentObj = SutdentListObj.FirstOrDefault();
                return StudentObj;
            }

            return new Student();
        }

        public void deleteById(string id)
        {
            List<Student> SutdentListObj = TableManagerObj.RetrieveEntity<Student>("RowKey eq '" + id + "'");
            Student StudentObj = SutdentListObj.FirstOrDefault();
            TableManagerObj.DeleteEntity<Student>(StudentObj);
        }
    }
}