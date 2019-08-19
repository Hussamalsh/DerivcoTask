using System;
//using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StudentDashBoard4.Models
{
    //[DataContract]
    public class Student
    {
        //[Required]
        //[DataMember(Name = "Id")]
        public Guid ID { get; set; }
        //[Required]
        //[Range(5, 50)]
        //[DataMember(Name = "FirstName")]
        public string FirstName { get; set; }
        //[Required]
        //[Range(5, 50)]
        //[DataMember(Name = "LastName")]
        public string LastName { get; set; }
        //[Required]
        //[Range(5, 200)]
        //[DataMember(Name = "Email")]
        public string Email { get; set; }
        //[Required]
        //[Range(5, 200)]
        //[DataMember(Name = "Address")]
        public string Address { get; set; }
        //[Required]
        //[Range(5, 50)]
        //[DataMember(Name = "City")]
        public string City { get; set; }
        //[Required]
        //[Range(5, 50)]
        //[DataMember(Name = "Zip")]
        public string ZIP { get; set; }
        //[Required]
        //[Range(5, 50)]
        //[DataMember(Name = "Phone")]
        public string Phone { get; set; }

        public string RowKey { get; set; }
    }
}
