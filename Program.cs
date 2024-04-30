// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using POSIndexer;
using POSIndexer.Migrations;
var messageHandler = new MQHandler();
messageHandler.AttachCreateEvent();
messageHandler.AttachRemoveEvent();
messageHandler.AttachOrderEvent();
var db = new InventoryReadDB();
db.Database.Migrate();
db.Dispose();
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
Console.WriteLine("Exitting...");
