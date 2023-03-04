using System.Text.Json;
using System.Threading.Channels;
using StackExchange.Redis;

namespace GrpcChat.ChatService.Redis;

public class RedisChatService: IChatService, IAsyncDisposable
{
    private readonly IConnectionMultiplexer _connection;
    private readonly string _channel;
    private readonly Lazy<RedisChatChannelManager> _lazyChatManager;

    public RedisChatService(IConnectionMultiplexer connection, string channel)
    {
        _connection = connection;
        _channel = channel;
        _lazyChatManager = new Lazy<RedisChatChannelManager>(() =>
        {
            var manager = new RedisChatChannelManager(connection, channel);
            manager.StartReceiveMessages();
            return manager;
        });
    }
    
    public async Task SendMessageAsync(string message, string username, CancellationToken token = default)
    {
        var subscriber = _connection.GetSubscriber();
        var serializedMessage = JsonSerializer.Serialize(new ReceivedMessage()
                                                         {
                                                             Message = message, Username = username
                                                         });
        await subscriber.PublishAsync(_channel, serializedMessage);
    }

    public IMessageReceiver CreateMessageReceiver()
    {
        var subscription = _connection.GetSubscriber();
        return new RedisMessageReceiver(_lazyChatManager.Value);
    }

    public async ValueTask DisposeAsync()
    {
        if (_lazyChatManager.IsValueCreated)
        {
            await _lazyChatManager.Value.DisposeAsync();
        }
    }
}