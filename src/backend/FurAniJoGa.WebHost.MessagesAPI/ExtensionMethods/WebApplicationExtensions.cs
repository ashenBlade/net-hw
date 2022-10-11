namespace MessagesAPI.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static void AddMiddlewaresForWebApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(x =>
        {
            x.WithOrigins("http://localhost:8080");
            x.AllowAnyHeader();
            x.AllowAnyMethod();
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.MapHub<ChatHub>("/chat");
    }
}