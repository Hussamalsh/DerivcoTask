﻿using Student.DataLib.RabbitMQ;
using System;
using System.Timers;

namespace StudentConsumerService
{
    public class Heartbeat
    {
        private readonly Timer _timer;
        private WorkerQueueConsumer workerQueueConsumer;

        public Heartbeat()
        {
            workerQueueConsumer = new WorkerQueueConsumer();
            _timer = new Timer(2000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            //call the consumer 
            workerQueueConsumer.Receive();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}