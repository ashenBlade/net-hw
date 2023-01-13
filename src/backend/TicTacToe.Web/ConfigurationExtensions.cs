using TicTacToe.Web.Options;

namespace TicTacToe.Web;

public static class ConfigurationExtensions
{
    public static FrontEndOptions GetFrontEndOptions(this IConfiguration config)
    {
        return new FrontEndOptions() {Urls = config.GetValue<string>("FRONTEND_URL")};
    }
    public static RabbitMqOptions GetRabbitMqOptions(this IConfiguration config)
    {
        return new RabbitMqOptions()
               {
                   Host = config.GetValue<string>("RMQ_HOST"),
                   Password = config.GetValue<string>("RMQ_PASSWORD"),
                   Username = config.GetValue<string>("RMQ_USERNAME"),
                   Exchange = config.GetValue<string>("RMQ_EXCHANGE")
               };
    }

    public static PostgresOptions GetPostgresOptions(this IConfiguration config)
    {
        return new PostgresOptions()
               {
                   Host = config.GetValue<string>("POSTGRES_HOST"),
                   Password = config.GetValue<string>("POSTGRES_PASSWORD"),
                   Username = config.GetValue<string>("POSTGRES_USERNAME"),
                   Database = config.GetValue<string>("POSTGRES_DB"),
                   Port = config.GetValue<int>("POSTGRES_PORT")
               };
    }
}