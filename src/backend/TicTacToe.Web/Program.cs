using MassTransit;
using TicTacToe.Web;
using TicTacToe.Web.Consumers.TicTacToe;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<UserStepEventConsumer>();
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
            e.ConfigureConsumer<UserStepEventConsumer>(ctx);
        });
        
        factory.ConfigureEndpoints(ctx);
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();