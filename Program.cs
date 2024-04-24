// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using POSIndexer;
using POSIndexer.Migrations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.HostName = Environment.GetEnvironmentVariable("MQ_HOSTNAME");
factory.UserName = Environment.GetEnvironmentVariable("MQ_USER");
factory.Password = Environment.GetEnvironmentVariable("MQ_PASS");

var exchangeName = "fanout_exchange";
var queueName = "carstorage-pos-indexer-queue";
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
var db = new InventoryReadDB();
channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
var queue = channel.QueueDeclare(queueName);
channel.QueueBind(queueName, exchangeName, "");

Console.WriteLine("Running..");
var consumer = new EventingBasicConsumer(channel);
db.Database.Migrate();
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var repository = new POSRepository();

    repository.UpdateDB(message,db);
};

channel.BasicConsume(queueName, true, consumer);
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
Console.WriteLine("Exitting...");
