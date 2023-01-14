using MassTransit;
using StackExchange.Redis;
using TicTacToe.Events;

namespace TicTacToe.RabbitListener.Consumers;

public class GameEndedEventConsumer: IConsumer<GameEndedEvent>
{
    private readonly IConnectionMultiplexer _multiplexer;
    private readonly ILogger<GameEndedEventConsumer> _logger;
    private IDatabase Database => _multiplexer.GetDatabase();

    public GameEndedEventConsumer(IConnectionMultiplexer multiplexer, ILogger<GameEndedEventConsumer> logger)
    {
        _multiplexer = multiplexer;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<GameEndedEvent> context)
    {
        var e = context.Message;
        await Database.SortedSetAddAsync("leaderboard",
                                         new SortedSetEntry[]
                                         {
                                             new(e.OwnerId.ToString(), e.OwnerRank),
                                             new(e.SecondPlayerId.ToString(), e.SecondPlayerRank)
                                         });
        _logger.LogInformation("Таблица лидеров обновлена после игры {GameId}", e.GameId);
    }
}