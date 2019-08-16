using Student.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentWebApi3.Controllers
{
    public class StudentController : ApiController
    {
        private IStudentsRepository studentsRepo;

        public StudentController()
        {
            studentsRepo = new StudentsRepository();
        }


        // GET: api/Student
        public async Task<IEnumerable<Student.Data.Models.Student>> GetAsync()
        {
            var studentList = await studentsRepo.GetStudentsAsync();
            return studentList;
        }

        // GET: api/Student/5
        public async Task<Student.Data.Models.Student> GetAsync(Guid id)
        {
            var student = await studentsRepo.GetStudentAsync(id);
            return student;
        }

        // POST: api/Student
        public async Task<HttpResponseMessage> PostAsync([FromBody]Student.Data.Models.Student newStudent)
        {
            var data = await studentsRepo.AddStudentAsync(newStudent);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<object>(new
                {
                    Message = "new student created"
                }, Configuration.Formatters.JsonFormatter)
            };
        }

        // PUT: api/Student/5
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody]Student.Data.Models.Student student)
        {
            student.ID = id;
            var data = await studentsRepo.UpdateStudentAsync(student);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<object>(new
                {
                    Message = "the student updated successfully"
                }, Configuration.Formatters.JsonFormatter)
            };
        }

        // DELETE: api/Student/5
        public HttpResponseMessage Delete(Guid id)
        {
            studentsRepo.DeleteStudent(id);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<object>(new
                {
                    Message = "the student deleted successfully"
                }, Configuration.Formatters.JsonFormatter)
            };
        }
    }
}
