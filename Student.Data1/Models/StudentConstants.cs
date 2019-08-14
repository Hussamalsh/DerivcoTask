using System;
using System.Collections.Generic;
using System.Text;

namespace Student.Data.Models
{
    public struct StudentConstants
    {
        public static string DefaultDataPath { get { return (Utility.getAbsolutePath("Data", true)); } }
    }
}
