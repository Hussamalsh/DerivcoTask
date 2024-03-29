﻿using Microsoft.WindowsAzure.Storage.Table;

namespace StudentFunctionApp.Models
{
    public class Student : TableEntity
    {
        public Student() { }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
    }
}
