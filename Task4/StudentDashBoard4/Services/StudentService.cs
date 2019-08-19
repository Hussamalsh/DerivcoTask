using Newtonsoft.Json;
using StudentDashBoard4.Common;
using StudentDashBoard4.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StudentDashBoard4.Services
{
    public class StudentService : IStudentService
    {
        /// <summary>
        /// Provides a base class for sending HTTP requests and receiving HTTP responses.
        /// </summary>
        private readonly HttpClient _httpclient;
        //WorkerQueueProducer _workerQueueProducer;
        QueueProducer QueueProducer;
        public StudentService(IHttpClientAccessor httpClientAccessor/*, WorkerQueueProducer workerQueueProducer*/)
        {
            _httpclient = httpClientAccessor.HttpClient;
            //_workerQueueProducer = workerQueueProducer;
            QueueProducer = new QueueProducer();
        }

        public async Task<IList<Student>> GetStudents()
        {
            HttpResponseMessage response = await _httpclient.GetAsync("api/student").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                var studentList = JsonConvert.DeserializeObject<List<Student>>(contents);
                return studentList;
            }
            return new List<Student>();
        }

        public async Task<Student> GetStudent(string id)
        {
            HttpResponseMessage response = await _httpclient.GetAsync("api/student/" + id).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                var student = JsonConvert.DeserializeObject<Student>(contents);
                return student;
            }
            return new Student();
        }

        public async Task<Student> AddStudent(Student student)
        {
            student.ID = Guid.NewGuid();
            //_workerQueueProducer.SendMessage(student,"add");
            await QueueProducer.SendMessagesAsync(student, "add").ConfigureAwait(false);
            /*string strPayload = JsonConvert.SerializeObject(student);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpclient.PostAsync("api/student/", c).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                return student;
            }*/
            return student;
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            bool updated = false;
            //_workerQueueProducer.SendMessage(student, "update");
            await QueueProducer.SendMessagesAsync(student, "update").ConfigureAwait(false);
            /*string strPayload = JsonConvert.SerializeObject(student);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpclient.PutAsync("api/student/" + student.ID, c).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                updated = true;
            }*/
            return updated;
        }

        public async Task deleteStudent(string id)
        {
            HttpResponseMessage response = await _httpclient.DeleteAsync("api/student/" + id).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }
    }
}
