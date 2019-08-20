using StudentDashboard.Common;
using StudentDashboard.Models;
using StudentDashboard.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace StudentDashboard
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;
        static ContainerHelper()
        {
            _container = new UnityContainer();

            string apiKey = ConfigurationManager.AppSettings["ApiKey"].ToString();
            string studentApiUrl = ConfigurationManager.AppSettings["StudentApiUrl"].ToString();
            string serviceBusConnectionString = ConfigurationManager.AppSettings["ServiceBusConnectionString"].ToString();
            string studentQueueName = ConfigurationManager.AppSettings["QueueName"].ToString();

            AppSettings appSetings = new AppSettings()
            {
                ApiKey = apiKey,
                StudentApiUrl = studentApiUrl,
                ServiceBusConnectionString = serviceBusConnectionString,
                QueueName = studentQueueName
            };

            // Instance registration
            IHttpClientAccessor httpClient = new DefaultHttpClientAccessor(appSetings);
            //_container.RegisterInstance(httpClient);

            IQueueProducer queueProducer = new QueueProducer(appSetings);

            //Register
            _container.RegisterType<IStudentService, StudentService>(new InjectionConstructor(httpClient, queueProducer));
            //_container.RegisterType<IQueueProducer, QueueProducer>(new InjectionConstructor(appSetings));

        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}
