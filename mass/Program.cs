using MassTransit;
using Model;
using MassTransit.AspNetCoreIntegration;
using MassTransit.ExtensionsDependencyInjectionIntegration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://guest:guest@localhost:5672");
       


    });
    
});


  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//var bus = Bus.Factory.CreateUsingRabbitMq(config =>
//{

//config.Host("amqp://guest:guest@localhost:5672");

//config.ReceiveEndpoint("temp-queue", c =>
//{
//    c.Handler<Order>(ctx =>
//    {
//        return Console.Out.WriteLineAsync(ctx.Message.Name);
//    });

//});


//});
//bus.Start();

//bus.Publish(new Order { Name = "test name" });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
