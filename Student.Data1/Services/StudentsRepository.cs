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
        IStudentPersistence StudentPersistence;
        public StudentsRepository()
        {
            StudentPersistence = new StudentPersistence();
        }

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
