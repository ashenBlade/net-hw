using System.Threading.Channels;
using StackExchange.Redis;

namespace GrpcChat.ChatService.Redis;

public class RedisMessageReceiver: IMessageReceiver
{
    private readonly RedisChatChannelManager _manager;

    internal RedisMessageReceiver(RedisChatChannelManager manager)
    {
        _manager = manager;
    }
    

    public async IAsyncEnumerable<ReceivedMessage> GetNextMessageAsync(CancellationToken token = default)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(token);
        while (!token.IsCancellationRequested)
        {
            var tcs = new TaskCompletionSource<ReceivedMessage>();
            await using var registration = token.Register(() => tcs.SetCanceled(token));

            void ManagerOnOnMessageReceived(object? _, ReceivedMessage message)
            {
                tcs.SetResult(message);
            }

            _manager.OnMessageReceived += ManagerOnOnMessageReceived;
            ReceivedMessage receivedMessage;
            try
            {
                receivedMessage = await tcs.Task;
            }
            catch (Exception)
            {
                _manager.OnMessageReceived -= ManagerOnOnMessageReceived;
                break;
            }
            
            yield return receivedMessage;
            _manager.OnMessageReceived -= ManagerOnOnMessageReceived;
        }
    }
}