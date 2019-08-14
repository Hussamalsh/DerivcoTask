using System;

namespace Student.Data.Models
{
    public class Student
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Phone { get; set; }
    }
}
