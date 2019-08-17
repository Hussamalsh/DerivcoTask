using Newtonsoft.Json;
using RabbitMQ.Client;
using Student.DataLib.Models;
using Student.DataLib.Persistence;
using System;
using System.IO;
using System.Text;

namespace Student.DataLib.RabbitMQ
{

    public class WorkerQueueConsumer : IDisposable
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel channel;
        private QueueingBasicConsumer consumer;
        private const string QueueName = "WorkerQueue_Queue";

        DapperHelper dapperHelper;

        public WorkerQueueConsumer()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            channel = _connection.CreateModel();


            channel.QueueDeclare(QueueName, true, false, false, null);
            channel.BasicQos(0, 1, false);

            consumer = new QueueingBasicConsumer(channel);

            channel.BasicConsume(QueueName, false, consumer);

            dapperHelper = new DapperHelper();
        }

        public void Receive()
        {
            string[] lines = new string[] { "Inside Receive => " + _factory.HostName + _factory.Endpoint + _factory .UserName};
            File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

            var ea = consumer.Queue.Dequeue();

            var json = Encoding.Default.GetString(ea.Body);
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

            Console.WriteLine(msgType);
            channel.BasicAck(ea.DeliveryTag, false);
            lines = new string[] { "end Receive" };
            File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            channel.Dispose();
            _connection.Dispose();
        }
    }
}
