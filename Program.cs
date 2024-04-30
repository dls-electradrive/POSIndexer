// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using POSIndexer;
using POSIndexer.Migrations;

var db = new InventoryReadDB();
db.Database.Migrate();
db.Dispose();

var messageHandler = new MQHandler();
messageHandler.AttachCreateEvent();
messageHandler.AttachRemoveEvent();
messageHandler.AttachOrderEvent();

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
Console.WriteLine("Exitting...");
