using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentConsumerService.Models;


namespace StudentConsumerService.Services
{
    public interface IStudentService
    {
        Task<Models.Student> AddStudent(Models.Student student);
        Task<bool> UpdateStudent(Models.Student student);
    }
}
