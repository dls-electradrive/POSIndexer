// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using POSIndexer;
using POSIndexer.Migrations;
using System;

var context = new InventoryReadDB();
if (context.Database.GetPendingMigrations().Any())
{
    Console.WriteLine($"Found {context.Database.GetPendingMigrations().Count()} migrations");
    context.Database.Migrate();
}
else
{
    Console.WriteLine("No migraitions to migrate");
}
context.Dispose();
var messageHandler = new MQHandler();
messageHandler.AttachCreateEvent();
messageHandler.AttachRemoveEvent();
messageHandler.AttachOrderEvent();

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
Console.WriteLine("Exitting...");
