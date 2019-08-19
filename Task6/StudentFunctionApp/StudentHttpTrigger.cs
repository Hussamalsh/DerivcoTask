using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;

namespace StudentFunctionApp
{
    public class StudentHttpTrigger
    {

        public StudentHttpTrigger()
        {


        }


        [FunctionName("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "student")] HttpRequest req,
            //[Table("student", "Student")] IQueryable<Student> studentList,
            [Table("student")] CloudTable cloudTable,
            ILogger log)
        {
            log.LogInformation("C# GetAllStudents HTTP trigger function processed a request.");

            TableQuery<Student> rangeQuery = new TableQuery<Student>()
                               .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Student"));

            var studentList = await cloudTable.ExecuteQuerySegmentedAsync(rangeQuery, null);
            return new OkObjectResult(studentList);
        }

       
        [FunctionName("GetStudent")]
        public async Task<IActionResult> GetStudent(
                 [HttpTrigger(AuthorizationLevel.Function, "get", Route = "student/{id}")] HttpRequest req,
                 [Table("student")] CloudTable cloudTable,
                 string id,
                 ILogger log)
        {
            log.LogInformation("C# GetStudent HTTP trigger function processed a request.");

            TableQuery<Student> rangeQuery = new TableQuery<Student>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,"Student"),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id)));

            var student = await cloudTable.ExecuteQuerySegmentedAsync(rangeQuery, null);

            return new OkObjectResult(student);
        }

        [FunctionName("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(
         [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "student/{id}")] HttpRequest req,
         [Table("student")] CloudTable cloudTable,
         string id,
         ILogger log)
        {
            log.LogInformation("C# DeleteStudent HTTP trigger function processed a request.");

            var deleteOperation = TableOperation.Delete(
                                  new TableEntity() { PartitionKey = "Student", RowKey = id, ETag = "*" });
            try
            {
                var deleteResult = await cloudTable.ExecuteAsync(deleteOperation);
            }
            catch (StorageException e) when (e.RequestInformation.HttpStatusCode == 404)
            {
                return new NotFoundResult();
            }
            return new OkResult();
        }

        [FunctionName("AddStudent")]
        [return: Table("student")]
        public async Task<Student> AddStudent(
                     [HttpTrigger(AuthorizationLevel.Function, "post", Route = "student")] HttpRequest req,
                     ILogger log)
        {
            log.LogInformation("C# AddStudent HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Student>(requestBody);

            return new Student { PartitionKey = "Student",
                                RowKey = Guid.NewGuid().ToString(),
                                FirstName = input.FirstName,
                                LastName = input.LastName,
                                Email = input.Email,
                                Address= input.Address,
                                City = input.City,
                                Zip=input.Zip,
                                Phone =input.Phone
                              };
        }

        [FunctionName("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(
             [HttpTrigger(AuthorizationLevel.Function, "put", Route = "student/{id}")] HttpRequest req,
             [Table("student")] CloudTable cloudTable,
             string id,
             ILogger log)
        {
            log.LogInformation("C# AddStudent HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<Student>(requestBody);

            var findOperation = TableOperation.Retrieve<Student>("Student", id);
            var findResult = await cloudTable.ExecuteAsync(findOperation);
            if (findResult.Result == null)
            {
                return new NotFoundResult();
            }

            var existingRow = (Student)findResult.Result;
            existingRow.FirstName = updated.FirstName;
            existingRow.LastName = updated.LastName;
            existingRow.Email = updated.Email;
            existingRow.Address = updated.Address;
            existingRow.City = updated.City;
            existingRow.Zip = updated.Zip;
            existingRow.Phone = updated.Phone;

            var replaceOperation = TableOperation.Replace(existingRow);
            await cloudTable.ExecuteAsync(replaceOperation);

            return new OkObjectResult(existingRow);
        }

        /*
        [FunctionName("GetStudent")]
        public async Task<IActionResult> GetStudent(
                 [HttpTrigger(AuthorizationLevel.Function, "get", Route = "student")] HttpRequest req,
                 string id,
                 ILogger log)
        {
            log.LogInformation("C# GetAllStudents HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }*/
    }
}
