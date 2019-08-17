using Newtonsoft.Json;
using RabbitMQ.Client;
using Student.DataLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.DataLib.RabbitMQ
{

    public class WorkerQueueConsumer
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private const string QueueName = "WorkerQueue_Queue";

        public WorkerQueueConsumer()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };

        }

        public void Receive()
        {
            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    channel.BasicQos(0, 1, false);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(QueueName, false, consumer);

                    while (true)
                    {
                        var ea = consumer.Queue.Dequeue();
                        var json = Encoding.Default.GetString(ea.Body);
                        var message = JsonConvert.DeserializeObject<Message>(json);

                        var msgType = message.Type;
                        var student = message.Student;
                        channel.BasicAck(ea.DeliveryTag, false);

                        //check the type
                        if (msgType.Equals("add"))
                        {
                            //add object to the db

                        }
                        else
                        {
                            //update object 
                        }
                    }
                }
            }
        }
    }
}
