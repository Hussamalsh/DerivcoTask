using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StudentConsumerService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp2
{
    /*
    public class Heartbeat
    {
        //private readonly Timer _timer;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel channel;
        private EventingBasicConsumer consumer;
        private const string QueueName = "WorkerQueue_Queue";

        private DapperHelper dapperHelper;

        public Heartbeat()
        {

            this.dapperHelper = new DapperHelper();


            this._factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            this._connection = _factory.CreateConnection();
            this.channel = _connection.CreateModel();

            //_timer = new Timer(1000) { AutoReset = true };
            //_timer.Elapsed += TimerElapsed;
        }

        /*private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            //call the consumer 
             
            workerQueueConsumer.Receive();
        }*/

      /*  public void Start()
        {
            //_timer.Start();
            string[] lines = new string[] { "Inside Start" };
            File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

            channel.QueueDeclare(QueueName, true, false, false, null);
            channel.BasicQos(0, 1, false);

            this.consumer = new EventingBasicConsumer(this.channel);

            channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);


            consumer.Received += (model, ea) =>
            {
                lines = new string[] { "Inside receive" };
                File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);
                //string response = null;

                var body = ea.Body;
                //var props = ea.BasicProperties;
                //var replyProps = channel.CreateBasicProperties();
                //replyProps.CorrelationId = props.CorrelationId;

                string receivedMessage = null;

                try
                {
                    receivedMessage = Encoding.UTF8.GetString(body);
                    ProcessMessage(receivedMessage);
                }
                catch (Exception e)
                {
                    // Received message is not valid.
                    /* WinLogger.Log.Error(
                         "Errror Processing Message: " + receivedMessage + " :" + e.Message);*/
   /*                 lines = new string[] { "Errror Processing Message: " + receivedMessage + " :" + e.Message };
                    File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);
                    //response = "";
                }
                finally
                {
                    lines = new string[] { "Inside finally" };
                    File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

                    //var responseBytes = Encoding.UTF8.GetBytes(response);
                    //channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                    //basicProperties: replyProps, body: responseBytes);

                    channel.BasicAck(deliveryTag: ea.DeliveryTag,multiple: false);
                }
            };

        }

        private void ProcessMessage(string receivedMessage)
        {
            var lines = new string[] { "recevied ProcessMessage => " + receivedMessage };
            File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

            var message = JsonConvert.DeserializeObject<Message>(receivedMessage);

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
        }

        public void Stop()
        {
            //_timer.Stop();
            var lines = new string[] { "Inside stop" };
            File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

            this._connection.Dispose();
            this.channel.Dispose();
        }
    }
*/

}
