using FurAniJoGa.WebHost.FileAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFileApi(builder.Configuration.GetS3FileServiceOptions(), 
                            builder.Configuration.GetRedisSettings(), 
                            builder.Configuration);

if (!builder.Environment.IsProduction())
{
    builder.Services.AddSwaggerGen();
    builder.Services.AddEndpointsApiExplorer();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    Console.WriteLine(builder.Configuration["FORUM_URL"]);
    policy.WithOrigins(builder.Configuration["FORUM_URL"])
          .AllowAnyHeader()
          .AllowCredentials()
          .AllowAnyMethod();
});

app.MapControllers();

app.Run();