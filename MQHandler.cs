﻿using Newtonsoft.Json;
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
    public class MQHandler
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        public MQHandler(IConfiguration configuration)
        {
            var messageQueueConfig = configuration.GetSection("MessageQueue");
            _factory = new ConnectionFactory();
            _factory.HostName = messageQueueConfig["Hostname"];
            _factory.UserName = messageQueueConfig["Username"];
            _factory.Password = messageQueueConfig["Password"];
            _connection = _factory.CreateConnection();
        }
        public void AttachCreateEvent()
        {
            var repo = new POSRepository();
            AttachQueueEvent("addcarqueue-indexer", "removeExchange-indexer", repo.AddCar);
        }
        public void AttachOrderEvent()
        {
            var repo = new POSRepository();
            AttachQueueEvent("updatecarqueue-indexer", "removeExchange-indexer", repo.UpdateCar);
        }
        public void AttachRemoveEvent()
        {
            var repo = new POSRepository();
            AttachQueueEvent("invalidatecarqueue-indexer", "removeExchange-indexer", repo.InvalidateCar);
        }
        private void AttachQueueEvent(string queueName, string exchangeName, Action<Car> function)
        {
            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
            channel.QueueDeclare(queueName);
            channel.QueueBind(queueName, exchangeName, "");
            channel = _connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var dto = JsonConvert.DeserializeObject<Car>(message);
                Console.WriteLine("Got event");
                function(dto);
            };
            channel.BasicConsume(queueName, true, consumer);

        }
    }
}
