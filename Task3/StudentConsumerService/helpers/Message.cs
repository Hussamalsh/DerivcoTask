using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentConsumerService.Models
{
    public class Message
    {
        public Student Student { get; set; }
        public string Type { get; set; }
    }
}
