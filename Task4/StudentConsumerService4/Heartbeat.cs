using Newtonsoft.Json;
using StudentConsumerService4.helpers;
using StudentConsumerService4.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentConsumerService4
{
    public class Heartbeat
    {
        private QueueConsumer queueConsumer;

        public Heartbeat()
        {
            queueConsumer = new QueueConsumer();
        }

        public void Start()
        {
            //_timer.Start();
            string[] lines = new string[] { "Inside Start" };
            File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

            try
            {
                //receivedMessage = Encoding.UTF8.GetString(body);
                //ProcessMessage(receivedMessage);
                queueConsumer.RegisterOnMessageHandlerAndReceiveMessages();
            }
            catch (Exception e)
            {
                // Received message is not valid.
                lines = new string[] { "Errror Processing Message: "  + " :" + e.Message };
                File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);
                //response = "";
            }
            finally
            {
                lines = new string[] { "Inside finally" };
                File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);
            }
        }

        public void Stop()
        {
            //_timer.Stop();
            var lines = new string[] { "Inside stop" };
            File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

            queueConsumer.Dispose();
        }
    }
}
