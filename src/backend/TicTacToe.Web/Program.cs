using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Web;
using TicTacToe.Web.Consumers.TicTacToe;
using TicTacToe.Web.JwtService;
using TicTacToe.Web.Managers;
using TicTacToe.Web.Models;
using TicTacToe.Web.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services
       .AddAuthentication()
       .AddJwtBearer(jwt =>
        {
            jwt.TokenValidationParameters = new TokenValidationParameters()
                                            {
                                                ValidateAudience = false,
                                                ValidateIssuer = false,
                                                IssuerSigningKey = SimpleJwtService.SecurityKey,
                                                ValidateIssuerSigningKey = true
                                            };
        });

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

builder.Services.AddSingleton(builder.Configuration.GetPostgresOptions());

builder.Services.AddDbContext<TicTacToeDbContext>((provider, options) =>
{
    var dbOptions = provider.GetRequiredService<PostgresOptions>();
    options.UseNpgsql(dbOptions.ToConnectionString());
});

builder.Services.AddIdentityCore<User>()
       .AddUserManager<TicTacUserManger>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors =>
{
    var frontEnd = builder.Configuration.GetFrontEndOptions();
    cors.WithOrigins(frontEnd.Urls.Split(','));
    cors.AllowAnyMethod();
    cors.AllowAnyHeader();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();