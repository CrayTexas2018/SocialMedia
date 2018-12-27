using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace User.Database
{
    class ServiceBusHelper
    {
        const string ServiceBusConnectionString = "Endpoint=sb://socialmediadev.servicebus.windows.net/;SharedAccessKeyName=app;SharedAccessKey=9HaZWvG40WgFscWgJ/QoT44KIPxnCCtxsg0lUVMh06I=";
        static IQueueClient queueClient;

        public static async Task SendMessageAsync(string queueName, string data)
        {
            // Create queue client
            queueClient = new QueueClient(ServiceBusConnectionString, queueName);

            string messageBody = data;
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await queueClient.SendAsync(message);
        }
    }
}
