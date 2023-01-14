using TicTacToe.RabbitListener.Options;
using TicTacToe.Web.Options;

namespace TicTacToe.Web;

public static class ConfigurationExtensions
{
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

    public static RedisOptions GetRedisOptions(this IConfiguration config)
    {
        return new RedisOptions() {Host = config.GetValue<string>("REDIS_HOST")};
    }
}