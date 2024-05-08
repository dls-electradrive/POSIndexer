// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using POSIndexer;
using POSIndexer.Migrations;
using POSIndexer.Models;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();

builder.Services.AddDbContext<InventoryReadDB>(options =>
    options.UseMySQL(configuration.GetConnectionString("Database")));
builder.Services.AddScoped<IPOSRepository, POSRepository>();
builder.Services.AddTransient<IMQHandler, MQHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<InventoryReadDB>();


if (db.Database.GetPendingMigrations().Any())
{
    Console.WriteLine($"Found {db.Database.GetPendingMigrations().Count()} migrations");
    db.Database.Migrate();
}
else
{
    Console.WriteLine("No migraitions to migrate");
}

app.MapGet("/clean-testdata", (InventoryReadDB dbcontext) =>
{
    var testcars = dbcontext.Cars.Where(x => x.Name.StartsWith("test-car")).ToList();
    dbcontext.Cars.RemoveRange(testcars);
    dbcontext.SaveChanges();

});
app.MapGet("/create-testdata", (InventoryReadDB dbcontext) =>
{

    var guid = Guid.NewGuid();
    var testCar = new Car
    {
        Name = "test-car" + guid,
        Id = guid,
        Color = "red",
        CreatedDate = DateTime.Now,
        Type = "TestCar",
        Parts = new List<Part>
        {
            new Part()
            {
                Id = Guid.NewGuid(),
                CarId = guid,
                CreatedDate = DateTime.Now,
                Name = "TestPart",
                Description = "This is a dummy part"
            }
        }
    };
    dbcontext.Cars.Add(testCar);
    dbcontext.SaveChanges();

});

app.UseSwagger();
app.UseSwaggerUI();

var messageHandler = scope.ServiceProvider.GetService<IMQHandler>();
messageHandler.AttachQueueEvent();
app.Run();