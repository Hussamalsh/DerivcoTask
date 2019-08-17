using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Student.Data.Models;
using Student.Data2.Persistence;
using Student.DataLib.Persistence;

namespace Student.Data.Repo
{
    public class StudentsRepository : IStudentsRepository
    {
        DapperHelper dapperHelper;
        public StudentsRepository()
        {
            dapperHelper = new DapperHelper();
        }

        public Task<Models.Student> AddStudentAsync(Models.Student student)
        {
            dapperHelper.ExecuteProcedure_AddStudent(student);
            return Task.FromResult(new Models.Student());
        }

        public void DeleteStudent(Guid studentId) => dapperHelper.ExecuteProcedure_DeleteById(studentId);

        public Task<Models.Student> GetStudentAsync(Guid id) => Task.FromResult(dapperHelper.ExecuteProcedure_GetById(id));

        public Task<List<Models.Student>> GetStudentsAsync() => Task.FromResult(dapperHelper.ExecuteProcedure_GetAllStudent());

        public Task<bool> UpdateStudentAsync(Models.Student student)
        {
            dapperHelper.ExecuteProcedure_UpdateStudent(student);
            return (Task.FromResult(true));
        }
    }
}
