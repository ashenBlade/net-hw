using GrpcChat.Database;
using GrpcChat.Domain;
using GrpcChat.TokenGenerator;
using GrpcChat.TokenGenerator.Jwt;
using GrpcChat.Auth.Web;
using GrpcChat.Auth.Web.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(builder.Configuration.GetJwtOptions());

builder.Services.AddOptions<DatabaseOptions>()
       .Bind(builder.Configuration.GetRequiredSection(DatabaseOptions.Key))
       .ValidateDataAnnotations()
       .ValidateOnStart();
builder.Services.AddDbContext<ChatDbContext>((sp, optionsBuilder) =>
{
    var options = sp.GetRequiredService<IOptions<DatabaseOptions>>();
    optionsBuilder.UseNpgsql(options.Value.ConnectionString);
});

builder.Services.AddIdentity<User, Role>(identity =>
        {
            identity.Password = new PasswordOptions()
                                {
                                    RequireDigit = false,
                                    RequiredLength = 6,
                                    RequireLowercase = false,
                                    RequireUppercase = false,
                                    RequiredUniqueChars = 1,
                                    RequireNonAlphanumeric = false
                                };
            identity.User = new UserOptions() {RequireUniqueEmail = true,};
        })
       .AddUserManager<UserManager<User>>()
       .AddSignInManager<SignInManager<User>>()
       .AddDefaultTokenProviders()
       .AddEntityFrameworkStores<ChatDbContext>();

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
                                                ValidateIssuer = true,
                                                ValidIssuer = options.Issuer,
                                                ValidateLifetime = false,
                                                IssuerSigningKey = options.CreateSecurityKey(),
                                                ValidateIssuerSigningKey = false
                                            };
        });

builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<JwtOptions>();
    return new JwtTokenOptions()
           {
               Key = options.Key,
               Audience = options.Audience,
               Issuer = options.Issuer,
               Lifetime = TimeSpan.FromHours(2)
           };
});
builder.Services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();

var app = builder.Build();

app.UseCors(options => options
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:3000"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();