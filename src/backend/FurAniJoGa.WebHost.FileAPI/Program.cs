using FurAniJoGa.WebHost.FileAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IFileService, S3FileService>();
builder.Services.AddSingleton(new S3FileServiceOptions()
                              {
                                  Bucket = "default-bucket",
                                  Host = new Uri("http://minio:9000"),
                                  Password = "minio_password",
                                  SecretKey = "minio_user"
                              });
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
// if (app.Environment.IsDevelopment())
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.Run();