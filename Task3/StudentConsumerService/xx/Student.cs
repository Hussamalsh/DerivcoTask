using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Student
    {
        //[Required]
        public Guid ID { get; set; }
        //[Required]
        //[Range(5, 50)]
        public string FirstName { get; set; }
        //[Required]
        //[Range(5, 50)]
        public string LastName { get; set; }
        //[Required]
        //[Range(5, 200)]
        public string Email { get; set; }
        //[Required]
        //[Range(5, 200)]
        public string Address { get; set; }
        //[Required]
        //[Range(5, 50)]
        public string City { get; set; }
        //[Required]
        //[Range(5, 50)]
        public string ZIP { get; set; }
        //[Required]
        //[Range(5, 50)]
        public string Phone { get; set; }
    }
}
