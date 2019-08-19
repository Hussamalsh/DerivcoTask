using StudentConsumerService4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentConsumerService4.Services
{
    public interface IStudentService
    {
        Task<Student> AddStudent(Student student);
        Task<bool> UpdateStudent(Student student);
    }
}
