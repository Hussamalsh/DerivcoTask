using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDashboard2.Services
{
    public interface IStudentService
    {
        Task<IList<Student.Data.Models.Student>> GetStudents();
        Task<Student.Data.Models.Student> GetStudent(Guid id);
        Task<Student.Data.Models.Student> AddStudent(Student.Data.Models.Student student);
        Task<bool> UpdateStudent(Student.Data.Models.Student student);
        Task deleteStudent(Guid id);
    }
}
