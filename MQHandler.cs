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
    public class MQHandler
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        public MQHandler()
        {
            _factory = new ConnectionFactory();
            _factory.HostName = Environment.GetEnvironmentVariable("MQ_HOSTNAME");
            _factory.UserName = Environment.GetEnvironmentVariable("MQ_USER");
            _factory.Password = Environment.GetEnvironmentVariable("MQ_PASS");
            _connection = _factory.CreateConnection();
        }
        public void AttachCreateEvent()
        {
            var repo = new POSRepository();
            AttachQueueEvent("addcarqueue-indexer", "removeExchange-indexer", _repository.AddCar);
        }
        public void AttachOrderEvent()
        {
            AttachQueueEvent("updatecarqueue-indexer", "removeExchange-indexer", _repository.UpdateCar);
        }
        public void AttachRemoveEvent()
        {
            AttachQueueEvent("invalidatecarqueue-indexer", "removeExchange-indexer", _repository.InvalidateCar);
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
                _repository = new POSRepository();
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
