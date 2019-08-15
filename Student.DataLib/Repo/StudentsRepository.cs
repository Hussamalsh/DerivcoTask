using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Student.Data.Models;
using Student.Data2.Persistence;

namespace Student.Data.Repo
{
    public class StudentsRepository : IStudentsRepository
    {
        SqlHelper sqlHelper;
        public StudentsRepository()
        {
            sqlHelper = new SqlHelper();
        }

        public Task<Models.Student> AddStudentAsync(Models.Student student)
        {
            sqlHelper.ExecuteProcedure_AddStudent(student);
            return Task.FromResult(new Models.Student());
        }

        public void DeleteStudent(Guid studentId)
        {
            //StudentPersistence.Delete(studentId);
        }

        public Task<Models.Student> GetStudentAsync(Guid id) => Task.FromResult(sqlHelper.ExecuteProcedure_GetById(id));

        public Task<List<Models.Student>> GetStudentsAsync() => Task.FromResult(sqlHelper.ExecuteProcedure_GetAllStudent());

        public Task<bool> UpdateStudentAsync(Models.Student student)
        {
            return (Task.FromResult(true));
        }
    }
}
