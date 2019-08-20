
namespace StudentDashboard.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Phone { get; set; }
        public string RowKey { get; set; }
        /*[JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
        [JsonProperty(PropertyName = "timestamp")]
        public DateTimeOffset Timestamp { get; set; }
        [JsonProperty(PropertyName = "eTag")]
        public string ETag { get; set; }*/
    }
}
