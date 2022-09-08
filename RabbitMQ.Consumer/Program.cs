using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
namespace RabbitMQ.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
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

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine("Message received successfully!");
                        Console.WriteLine("Message:" + message);
                    };

                    channel.BasicConsume(queue: "logs_error",
                        autoAck: true,
                        consumer: consumer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error trying to process the message!");
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
