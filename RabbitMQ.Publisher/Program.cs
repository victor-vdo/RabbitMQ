using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
namespace RabbitMQ.Publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
      
                var number = 200000000000;
                var number2 = short.Parse(number.ToString());
            }
            catch (Exception ex)
            {
                var exception = new ExceptionFilter();
                exception.Message = ex.Message;
                exception.StackTrace = ex.StackTrace;
                var json = JsonConvert.SerializeObject(exception);

                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "logs_error",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    string message = json;

                   var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "logs_error",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine("Message sent successfully!");
                    Console.WriteLine("Message:" + message);

                }
            }
        }
    }
}
