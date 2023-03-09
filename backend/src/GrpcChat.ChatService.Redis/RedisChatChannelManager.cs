using System.Text.Json;
using StackExchange.Redis;

namespace GrpcChat.ChatService.Redis;

internal class RedisChatChannelManager: IAsyncDisposable
{
    private readonly IConnectionMultiplexer _multiplexer;
    private readonly string _channel;
    private ISubscriber? _subscriber;
    private Action<RedisChannel, RedisValue>? _handler;
    public event EventHandler<ReceivedMessage>? OnMessageReceived;
    
    public RedisChatChannelManager(IConnectionMultiplexer multiplexer, string channel)
    {
        _multiplexer = multiplexer;
        _channel = channel;
    }

    public  void StartReceiveMessages()
    {
        _subscriber = _multiplexer.GetSubscriber();
        _handler = (_, value) =>
        {
            var message = JsonSerializer.Deserialize<ReceivedMessage>(value!);
            OnMessageReceived?.Invoke(this, message!);
        };
         _subscriber.Subscribe(_channel, _handler);
    }

    public async ValueTask DisposeAsync()
    {
        if (_subscriber is not null)
        {
            await _subscriber.UnsubscribeAsync(_channel, _handler);
        }
    }
}