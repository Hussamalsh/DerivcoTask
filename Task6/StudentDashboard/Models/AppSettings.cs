using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDashboard.Models
{
    public class AppSettings
    {
        public string StudentApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}
