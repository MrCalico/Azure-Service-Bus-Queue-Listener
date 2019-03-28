using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener
{
    class Program
    {
        private static string topic = "trade-alerts";
        //private static string subscription = "listener";
        static void Main(string[] args)
        {

            if (args.Length > 0)  topic = args[0];
            Console.WriteLine($"Listening to: {topic}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("-----   This is a listener   -----");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Gray;

            var serviceBusConnectionString = ConfigurationManager.AppSettings["serviceBus"];

            //var subscriptionClient = new SubscriptionClient(serviceBusConnectionString, topic, subscription);
            //subscriptionClient.RegisterMessageHandler(async (msg, cancelationToken) =>

            var queueClient = new QueueClient(serviceBusConnectionString, topic); //, ReceiveMode.PeekLock);
            queueClient.RegisterMessageHandler(async (msg, exception) =>
            {
                var body = Encoding.UTF8.GetString(msg.Body);
                Console.WriteLine(body);

                await Task.CompletedTask;
            },
            async exception =>
            {
                await Task.CompletedTask;
                Console.WriteLine(exception);
                // log exception
            }
            );
            Console.WriteLine("Hit Enter to Continue - ");
            Console.ReadLine();
        }
    }
}
