using TicTacToe.Web.Options;

namespace TicTacToe.Web;

public static class ConfigurationOptions
{
    public static RabbitMqOptions GetRabbitMqOptions(this IConfiguration config)
    {
        
        return new RabbitMqOptions()
               {
                   Host = config.GetValue<string>("RMQ_HOST"),
                   Password = config.GetValue<string>("RMQ_PASS"),
                   Username = config.GetValue<string>("RMQ_USERNAME"),
                   Exchange = config.GetValue<string>("RMQ_EXCHANGE")
               };
    }
}