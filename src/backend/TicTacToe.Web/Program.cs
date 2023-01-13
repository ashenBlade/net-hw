using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TicTacToe.Web;
using TicTacToe.Web.Consumers.TicTacToe;
using TicTacToe.Web.GameRepository;
using TicTacToe.Web.Hubs;
using TicTacToe.Web.JwtService;
using TicTacToe.Web.Managers;
using TicTacToe.Web.Models;
using TicTacToe.Web.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddMvcCore()
       .AddAuthorization();
builder.Services
       .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

builder.Services.AddIdentityCore<User>(identity =>
        {
            identity.Password = new PasswordOptions()
                                {
                                    RequireDigit = false,
                                    RequiredLength = 1,
                                    RequireLowercase = false,
                                    RequireUppercase = false,
                                    RequireNonAlphanumeric = false,
                                };
            identity.User = new UserOptions() {RequireUniqueEmail = false,};
        })
       .AddUserManager<TicTacUserManger>()
       .AddSignInManager()
       .AddDefaultTokenProviders()
       .AddEntityFrameworkStores<TicTacToeDbContext>();

builder.Services.AddScoped<IJwtService, SimpleJwtService>();
builder.Services.AddScoped<IGameRepository, DatabaseGameRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                                      {
                                          In = ParameterLocation.Header,
                                          Description = "JWT токен",
                                          Name = "Authorization",
                                          Type = SecuritySchemeType.ApiKey,
                                          
                                          
                                      });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                             {
                                 {new OpenApiSecurityScheme()
                                  {
                                      Reference = new OpenApiReference()
                                                  {
                                                      Type = ReferenceType.SecurityScheme,
                                                      Id = "Bearer"
                                                  }
                                  },
                                     Array.Empty<string>()
                                 }
                             });
});
var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

{
    await using var scope = app.Services.CreateAsyncScope();
    var provider = scope.ServiceProvider;
    var context = provider.GetRequiredService<TicTacToeDbContext>();
    await context.Database.EnsureCreatedAsync();
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

app.MapHub<TicTacToeHub>("/game");
app.MapControllers();

app.Run();