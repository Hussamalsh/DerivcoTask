﻿using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using StudentConsumerService4.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentConsumerService4.helpers
{
    public class QueueConsumer : IDisposable
    {
        // Connection String for the namespace can be obtained from the Azure portal under the 
        // 'Shared Access policies' section.
        private const string ServiceBusConnectionString = "Endpoint=sb://studenthuss.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=8Q1gXjw/+tCfZO6Fko5YLjrglMY3GSamc8y1IpaV2cA=";
        private const string QueueName = "student";
        private IQueueClient queueClient;

        private IStudentService studentService;

        private TelemetryClient telemetry;
        private TelemetryConfiguration configuration;

        public QueueConsumer()
        {
            // you may use different options to create configuration as shown later in this article
            configuration = TelemetryConfiguration.CreateDefault();
            configuration.InstrumentationKey = "92ca92dc-9095-447b-9e53-4a8af43a4039";
            configuration.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());
            telemetry = new TelemetryClient(configuration);
            telemetry.InstrumentationKey = "92ca92dc-9095-447b-9e53-4a8af43a4039";
            InitializeDependencyTracking(configuration);
            //telemetry.Context.User.Id = "...";
            //telemetry.Context.Device.Id = "...";


            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            studentService = new StudentService(new DefaultHttpClientAccessor());
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            // Configure the MessageHandler Options in terms of exception handling, number of concurrent messages to deliver etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of Concurrent calls to the callback `ProcessMessagesAsync`, set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = false
            };

            // Register the function that will process messages
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            // Process the message
            //Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            //Use Service
            string receivedMessage = Encoding.UTF8.GetString(message.Body);
            ProcessMessage(receivedMessage);

            telemetry.TrackEvent("Received new msg => "+receivedMessage);

            // Complete the message so that it is not received again.
            // This can be done only if the queueClient is created in ReceiveMode.PeekLock mode (which is default).
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);

            // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
            // If queueClient has already been Closed, you may chose to not call CompleteAsync() or AbandonAsync() etc. calls 
            // to avoid unnecessary exceptions.
        }

        private void ProcessMessage(string receivedMessage)
        {
            var lines = new string[] { "recevied ProcessMessage => " + receivedMessage };
            File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

            var message = JsonConvert.DeserializeObject<Models.Message>(receivedMessage);

            var msgType = message.Type;
            var student = message.Student;

            //check the type
            if (msgType.Equals("add"))
            {
                //add object to the db
                //dapperHelper.ExecuteProcedure_AddStudent(student);
                studentService.AddStudent(student);
            }
            else
            {
                //update object 
                //dapperHelper.ExecuteProcedure_UpdateStudent(student);
                studentService.UpdateStudent(student);
            }
        }


        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            telemetry.TrackException(exceptionReceivedEventArgs.Exception);
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            queueClient.CloseAsync().Wait();
            // before exit, flush the remaining data
            telemetry.Flush();
        }

        private DependencyTrackingTelemetryModule InitializeDependencyTracking(TelemetryConfiguration configuration)
        {
            var module = new DependencyTrackingTelemetryModule();

            // prevent Correlation Id to be sent to certain endpoints. You may add other domains as needed.
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.windows.net");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.chinacloudapi.cn");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.cloudapi.de");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.usgovcloudapi.net");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("localhost");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("127.0.0.1");

            // enable known dependency tracking, note that in future versions, we will extend this list. 
            // please check default settings in https://github.com/Microsoft/ApplicationInsights-dotnet-server/blob/develop/Src/DependencyCollector/DependencyCollector/ApplicationInsights.config.install.xdt

            module.IncludeDiagnosticSourceActivities.Add("Microsoft.Azure.ServiceBus");
            module.IncludeDiagnosticSourceActivities.Add("Microsoft.Azure.EventHubs");

            // initialize the module
            module.Initialize(configuration);

            return module;
        }
    }
}
