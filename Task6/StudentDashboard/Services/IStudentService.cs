using StudentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentDashboard.Services
{
    public interface IStudentService
    {
        Task<IList<Student>> GetStudents();
        Task<Student> GetStudent(string id);
        Task<Student> AddStudent(Student student);
        Task<bool> UpdateStudent(Student student);
        Task deleteStudent(string id);
    }
}
