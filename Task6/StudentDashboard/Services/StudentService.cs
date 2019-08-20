using Newtonsoft.Json;
using StudentDashboard.Common;
using StudentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StudentDashboard.Services
{
    public class StudentService : IStudentService
    {
        /// <summary>
        /// Provides a base class for sending HTTP requests and receiving HTTP responses.
        /// </summary>
        private readonly HttpClient _httpclient;
        private readonly IQueueProducer QueueProducer;
        public StudentService(IHttpClientAccessor httpClientAccessor, IQueueProducer queueProducer)
        {
            _httpclient = httpClientAccessor.HttpClient;
            QueueProducer = queueProducer;
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
                var studentList = JsonConvert.DeserializeObject<List<Student>>(contents);
                return studentList[0];
            }
            return new Student();
        }

        public async Task<Student> AddStudent(Student student)
        {
            await QueueProducer.SendMessagesAsync(student, "add").ConfigureAwait(false);
            return student;

            //student.RowKey = Guid.NewGuid().;
            //_workerQueueProducer.SendMessage(student,"add");
            /*string strPayload = JsonConvert.SerializeObject(student);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpclient.PostAsync("api/student/", c).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                return student;
            }*/
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            bool updated = false;
            await QueueProducer.SendMessagesAsync(student, "update").ConfigureAwait(false);
            return updated;

            //_workerQueueProducer.SendMessage(student, "update");
            /*string strPayload = JsonConvert.SerializeObject(student);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpclient.PutAsync("api/student/" + student.ID, c).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                updated = true;
            }*/
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
