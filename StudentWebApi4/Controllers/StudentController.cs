using StudentWebApi4.Models;
using StudentWebApi4.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentWebApi4.Controllers
{
    public class StudentController : ApiController
    {
        private StudentRepository studentRepository;

        public StudentController()
        {
            studentRepository = new StudentRepository();
        }

        // GET: api/Student
        public IEnumerable<Student> Get()
        {
            var studentList = studentRepository.getallStudents();
            return studentList;
        }

        // GET: api/Student/5
        public Student Get(string id)
        {
            var student = studentRepository.getById(id);
            return student;
        }

        // POST: api/Student
        public HttpResponseMessage Post([FromBody]Student newStudent)
        {
            studentRepository.addUpdateStudent("", newStudent);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<object>(new
                {
                    Message = "new student created"
                }, Configuration.Formatters.JsonFormatter)
            };
        }

        // PUT: api/Student/5
        public HttpResponseMessage Put(string id, [FromBody]Student student)
        {
            studentRepository.addUpdateStudent(id, student);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<object>(new
                {
                    Message = "the student updated successfully"
                }, Configuration.Formatters.JsonFormatter)
            };

        }

        // DELETE: api/Student/5
        public HttpResponseMessage Delete(string id)
        {
            studentRepository.deleteById(id);

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
