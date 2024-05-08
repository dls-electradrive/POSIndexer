using Newtonsoft.Json;
using POSIndexer.Migrations;
using POSIndexer.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POSIndexer
{
    public class MQHandler : IMQHandler
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IPOSRepository _posRepo;
        private readonly IConfiguration _configuration;
        public MQHandler(IConfiguration configuration, IPOSRepository posRepo)
        {
            _configuration = configuration;
            var messageQueueConfig = _configuration.GetSection("MessageQueue");
            _factory = new ConnectionFactory();
            _factory.HostName = messageQueueConfig["Hostname"];
            _factory.UserName = messageQueueConfig["Username"];
            _factory.Password = messageQueueConfig["Password"];
            _connection = _factory.CreateConnection();
            _posRepo = posRepo;
        }

        public void AttachQueueEvent()
        {
            var queueName = "newStandardCar-indexer-queue";
            var exchangeName = "newCar-exchange";
            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
            channel.QueueDeclare(queueName);
            channel.QueueBind(queueName, exchangeName, "test");
            channel = _connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var route = ea.RoutingKey;
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var dto = JsonConvert.DeserializeObject<Car>(message);
                Console.WriteLine("--------------------------");
                Console.WriteLine("Got event");
                Console.WriteLine($"Route: {route}");
                Console.WriteLine("Object:");
                Console.WriteLine(message);
                switch (route)
                {
                    case "standard":
                        _posRepo.UpdateCar(dto);
                        break;
                    case "custom":
                    case "created":
                        _posRepo.AddCar(dto);
                        break;
                    default:
                        break;
                }
            };
            channel.BasicConsume(queueName, true, consumer);

        }
    }
}
