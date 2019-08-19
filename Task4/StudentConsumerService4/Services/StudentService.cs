using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using StudentConsumerService4.helpers;

namespace StudentConsumerService4.Services
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

        public async Task<Models.Student> AddStudent(Models.Student student)
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
            return new Models.Student();
        }

        public async Task<bool> UpdateStudent(Models.Student student)
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
    }
}
