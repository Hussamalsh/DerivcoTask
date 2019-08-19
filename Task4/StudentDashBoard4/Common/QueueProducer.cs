using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using StudentDashBoard4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDashBoard4.Common
{
    public class QueueProducer : IDisposable
    {
        // Connection String for the namespace can be obtained from the Azure portal under the 
        // 'Shared Access policies' section.
        private const string ServiceBusConnectionString = "Endpoint=sb://studenthuss.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=8Q1gXjw/+tCfZO6Fko5YLjrglMY3GSamc8y1IpaV2cA=";
        private const string QueueName = "student";
        private IQueueClient queueClient;

        public QueueProducer()
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
        }

        public async Task SendMessagesAsync(Student student, string type)
        {
            try
            {
                // Create a new message to send to the queue
                var msg = new Models.Message() {Type = type, Student = student };
                var payload = JsonConvert.SerializeObject(msg);
                var message = new Microsoft.Azure.ServiceBus.Message(Encoding.UTF8.GetBytes(payload));
                // Send the message to the queue
                await queueClient.SendAsync(message).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }

        public void Dispose()
        {
            queueClient.CloseAsync().Wait();
        }


    }
}
