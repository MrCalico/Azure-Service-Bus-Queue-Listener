using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;

namespace Publisher
{
    public class Range {
        public string symbol;
        public string status;
        public string trade;
        public decimal limit;
        public decimal stop;
    }

    class Program
    {
        private static string topic = "trade-alerts";
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-----   This is a publisher   -----");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            var loop = true;
            do
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Write you message:");

                var message = Console.ReadLine();
                if (message == "q")
                    break;

                var serviceBusConnectionString = ConfigurationManager.AppSettings["serviceBus"];
                //var topicClient = new TopicClient(serviceBusConnectionString, topic);
                var queueClient = new QueueClient(serviceBusConnectionString, topic);
                Range range = new Range() { symbol = "TEST", limit = 100m, stop = 99m, status = "A-Up", trade = "Buy" };
                //var body = Encoding.UTF8.GetBytes(message);
                //var body = Encoding.UTF8.GetBytes(range);
                //var busMessage = new Message(body);
                //var json = new <Range>JsonSerializer(range);
                var busMessage = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(range)));
                //topicClient.SendAsync(busMessage).GetAwaiter().GetResult();
                queueClient.SendAsync(busMessage).GetAwaiter().GetResult();
                

            } while (loop);


        }
    }
}
