using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace FilterBasedRouting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Sending Messages");
           
            Publisher.Send("Orange message", "orange");
            Publisher.Send("Green message", "green");
            Publisher.Send("black message", "black");


        }

    }
    public static class Publisher
    {
        public static void Send(string message, string route)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.ExchangeDeclare(exchange: "direct_logs",
                                        type: "direct");

                var routingKey = "direct." + route;
                var theMessage = message;
                var body = Encoding.UTF8.GetBytes(theMessage);
                channel.BasicPublish(exchange: "direct_logs",
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);
            }

        }
    }
}