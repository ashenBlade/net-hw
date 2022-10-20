using FurAniJoGa.WebHost.FileAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFileApi(builder.Configuration.GetS3FileServiceOptions());


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
    policy.WithOrigins(builder.Configuration["FORUM_URL"]);
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
});

app.MapControllers();

app.Run();