using GrpcChat.Chat.Web;
using GrpcChat.Chat.Web.Options;
using GrpcChat.Chat.Web.Services;
using GrpcChat.ChatService;
using GrpcChat.ChatService.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
       .AddJwtBearer(jwt =>
        {
            var options = builder.Configuration.GetJwtOptions();
            jwt.RequireHttpsMetadata = false;
            jwt.TokenValidationParameters = new TokenValidationParameters()
                                            {
                                                ValidateAudience = false,
                                                ValidAudience = options.Audience,
                                                ValidateIssuer = false,
                                                ValidIssuer = options.Issuer,
                                                ValidateLifetime = false,
                                                IssuerSigningKey = options.CreateSecurityKey(),
                                                ValidateIssuerSigningKey = false
                                            };               
        });
builder.Services.AddAuthorization();
builder.Services.AddGrpc();

builder.Services
       .AddOptions<RedisOptions>()
       .Bind(builder.Configuration.GetRequiredSection(RedisOptions.Key))
       .ValidateDataAnnotations()
       .ValidateOnStart();

builder.Services.AddSingleton<IChatService>(sp =>
{
    var redis = sp.GetRequiredService<IOptions<RedisOptions>>();
    var multiplexer = ConnectionMultiplexer.Connect(redis.Value.Servers);
    return new RedisChatService(multiplexer, redis.Value.Channel);
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<GrpcChatService>();

app.Run();