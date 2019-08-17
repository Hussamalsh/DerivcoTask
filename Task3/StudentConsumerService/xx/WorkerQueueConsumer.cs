using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class WorkerQueueConsumer : IDisposable
    {
        private ConnectionFactory _factory;
        private IConnection _connection;

        private const string QueueName = "WorkerQueue_Queue";

        DapperHelper dapperHelper;

        public WorkerQueueConsumer()
        {
            dapperHelper = new DapperHelper();
        }

        public  void Receive()
        {
            string[] lines = new string[] { "Inside Receive => "};
            File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    channel.BasicQos(0, 1, false);

                    var consumer = new EventingBasicConsumer(channel);

                    BasicGetResult result = channel.BasicGet(QueueName, true);
                    if (result != null)
                    {
                        string data =
                        Encoding.UTF8.GetString(result.Body);
                        Console.WriteLine(data);
                    }

                    /*consumer.Received += (model, ea) =>
                    {
                        lines = new string[] { "recevied while=> " };
                        File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

                        var body = ea.Body;
                        //var message = Encoding.UTF8.GetString(body);
                        //Console.WriteLine(" [x] Received {0}", message);
                        var json = Encoding.Default.GetString(body);
                        lines = new string[] { "recevied while json => " + json };
                        File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

                        var message = JsonConvert.DeserializeObject<Message>(json);

                        var msgType = message.Type;
                        var student = message.Student;

                        //check the type
                        if (msgType.Equals("add"))
                        {
                            //add object to the db
                            dapperHelper.ExecuteProcedure_AddStudent(student);
                        }
                        else
                        {
                            //update object 
                            dapperHelper.ExecuteProcedure_UpdateStudent(student);
                        }

                        channel.BasicAck(ea.DeliveryTag, false);

                        lines = new string[] { "end Receive" };
                        File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

                    };*/

                    channel.BasicConsume(QueueName, true, consumer);
                }
            }
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            //channel.Dispose();
            //_connection.Dispose();
        }
    }
}
