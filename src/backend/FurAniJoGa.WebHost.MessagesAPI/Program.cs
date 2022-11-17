using MessagesAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServicesForMessagesApi(builder.Configuration);

var app = builder.Build();

app.AddMiddlewaresForWebApplication();

app.Run();