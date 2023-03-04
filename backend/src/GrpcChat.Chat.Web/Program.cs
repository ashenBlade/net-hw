using GrpcChat.Chat.Web;
using GrpcChat.Chat.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<GrpcChatService>();

app.Run();