using MassTransit;
using StackExchange.Redis;
using TicTacToe.RabbitListener.Consumers;
using TicTacToe.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<GameEndedEventConsumer>();
    configurator.UsingRabbitMq((ctx, factory) =>
    {
        var rabbitOptions = builder.Configuration.GetRabbitMqOptions();
        factory.Host(rabbitOptions.Host, "/", h =>
        {
            h.Username(rabbitOptions.Username);
            h.Password(rabbitOptions.Password);
        });

        factory.ReceiveEndpoint(e =>
        {
            e.Bind(rabbitOptions.Exchange);
            e.ConfigureConsumer<GameEndedEventConsumer>(ctx);
        });
        
        factory.ConfigureEndpoints(ctx);
    });
});

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetRedisOptions().Host));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();