using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student.Data.Repo
{
    public interface IStudentsRepository
    {
        Task<List<Models.Student>> GetStudentsAsync();
        Task<Models.Student> GetStudentAsync(Guid id);
        Task<Models.Student> AddStudentAsync(Models.Student student);
        Task<bool> UpdateStudentAsync(Models.Student student);
        void DeleteStudent(Guid studentId);
    }
}
