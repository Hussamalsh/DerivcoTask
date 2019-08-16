using Newtonsoft.Json;
using StudentDashboard2.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StudentDashboard2.Services
{
    public class StudentService : IStudentService
    {
        /// <summary>
        /// Provides a base class for sending HTTP requests and receiving HTTP responses.
        /// </summary>
        private readonly HttpClient _httpclient;

        public StudentService(IHttpClientAccessor httpClientAccessor)
        {
            _httpclient = httpClientAccessor.HttpClient;
        }

        public async Task<IList<Student.Data.Models.Student>> GetStudents()
        {
            HttpResponseMessage response = await _httpclient.GetAsync("api/student").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                var studentList = JsonConvert.DeserializeObject<List<Student.Data.Models.Student>>(contents);
                return studentList;
            }
            return new List<Student.Data.Models.Student>();
        }

        public async Task<Student.Data.Models.Student> GetStudent(Guid id)
        {
            HttpResponseMessage response = await _httpclient.GetAsync("api/student/" + id).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                var student = JsonConvert.DeserializeObject<Student.Data.Models.Student>(contents);
                return student;
            }
            return new Student.Data.Models.Student();
        }

        public async Task<Student.Data.Models.Student> AddStudent(Student.Data.Models.Student student)
        {
            student.ID = Guid.NewGuid();
            string strPayload = JsonConvert.SerializeObject(student);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpclient.PostAsync("api/student/", c).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                return student;
            }
            return new Student.Data.Models.Student();
        }

        public async Task<bool> UpdateStudent(Student.Data.Models.Student student)
        {
            bool updated = false;
            string strPayload = JsonConvert.SerializeObject(student);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpclient.PutAsync("api/student/" + student.ID, c).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                updated = true;
            }
            return updated;
        }

        public async Task deleteStudent(Guid id)
        {
            HttpResponseMessage response = await _httpclient.DeleteAsync("api/student/" + id).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
