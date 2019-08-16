using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.DataLib.RabbitMQ
{
    public class WorkerQueueProducer
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string QueueName = "WorkerQueue_Queue";

        public WorkerQueueProducer()
        {
            CreateConnection();
        }

        public void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();

            _model.QueueDeclare(QueueName, true, false, false, null);
        }

        public void SendMessage(Data.Models.Student message, string type)
        {
            var obj = new { Message = message, Type = type };
            var payload = JsonConvert.SerializeObject(obj);
            _model.BasicPublish("", QueueName,true, null, Encoding.ASCII.GetBytes(payload));
        }

    }

}
