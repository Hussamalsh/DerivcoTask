using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Student.Data.Models;
using Student.Data.Persistence;

namespace Student.Data.Services
{
    public class StudentsRepository : IStudentsRepository
    {
        //public ObservableCollection<Models.Student> StudentList { get; set; }
        IStudentPersistence StudentPersistence;
       // public List<Models.Student> StudentList { get; set; }
        public StudentsRepository()
        {
            //StudentList = new ObservableCollection<Models.Student>();
            //StudentList = new List<Models.Student>();
            StudentPersistence = new StudentPersistence();
            //addTestData();
        }

       /* public void addTestData()
        {
            Models.Student p1 = new Models.Student
            {
                ID = Guid.NewGuid(),
                FirstName = "Hussam",
                LastName = "Alshammari",
                City = "london",
                Address = "Flat 1 street",
                Email = "zain@hussam.com",
                ZIP = "1234",
                Phone = "0757084000"
            };

            Models.Student p2 = new Models.Student
            {
                ID = Guid.NewGuid(),
                FirstName = "zain",
                LastName = "yousif",
                City = "london",
                Address = "big street",
                Email = "hussam@hussam.com",
                ZIP = "1001",
                Phone = "0700000000"
            };

            StudentList.Add(p1);
            StudentList.Add(p2);
        }*/

        public Task<Models.Student> AddStudentAsync(Models.Student student)
        {
            student.ID = Guid.NewGuid();
            StudentPersistence.Add(student);
            return Task.FromResult(student);
        }

        public void DeleteStudent(Guid studentId)
        {
            StudentPersistence.Delete(studentId);
        }

        public Task<Models.Student> GetStudentAsync(Guid id)
        {
            return (Task.FromResult(StudentPersistence.Get(id)));
        }

        public Task<List<Models.Student>> GetStudentsAsync()
        {
            return Task.FromResult(StudentPersistence.List());
        }

        public Task<bool> UpdateStudentAsync(Models.Student student)
        {
            return (Task.FromResult(StudentPersistence.Update(student)));
        }
    }
}
