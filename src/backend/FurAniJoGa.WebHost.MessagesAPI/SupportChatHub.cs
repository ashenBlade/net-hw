using System.Text.RegularExpressions;
using FurAniJoGa.RabbitMq.Contracts.Events;
using MassTransit;
using MessagesAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace MessagesAPI;

public class SupportChatHub : Hub
{
    public const string PublishMessageMethodName = "publishMessage";
    public const string EndChatFunctionName = "endChat";
    public const string LoginFunctionName = "login";

    public const string OnChatStartedFunctionName = "onChatStarted";
    public const string OnChatEndedFunctionName = "onChatEnded";

    private readonly Forum _forum;
    private readonly IBus _bus;
    private readonly ILogger<SupportChatHub> _logger;

    public SupportChatHub(IBus bus, ILogger<SupportChatHub> logger, Forum forum)
    {
        _bus = bus;
        _logger = logger;
        _forum = forum;
        _forum.ChatEnded += ForumOnChatEnded;
        _forum.ChatStarted += ForumOnChatStarted;
    }

    private async void ForumOnChatStarted(object? sender, ChatEventArgs args)
    {
        var (chatId, user, support) = args;
        _logger.LogInformation("Начался чат {ChatId}", chatId);
        await Groups.AddToGroupAsync(user, chatId);
        await Groups.AddToGroupAsync(support, chatId);
        _logger.LogInformation("Пользователь {UserId} и поддержка {SupportId} добавлены в чат {ChatId}", user, support, chatId);
        await Clients.Client(user).SendAsync(OnChatStartedFunctionName, "user");
        await Clients.Client(support).SendAsync(OnChatStartedFunctionName, "support");
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _forum.DisconnectUser(Context.ConnectionId);
    }

    private async void ForumOnChatEnded(object? sender, ChatEventArgs? args)
    {
        var (chatId, user, support) = args;
        _logger.LogInformation("Чат {ChatId} заканчивается", chatId);
        await Clients.Group(chatId).SendAsync(OnChatEndedFunctionName);
        await Task.WhenAll(Groups.RemoveFromGroupAsync(user, chatId),
                           Groups.RemoveFromGroupAsync(support, chatId));
        _forum.ForceUpdateChats();
    }

    [HubMethodName(EndChatFunctionName)]
    public async Task EndChat()
    {
        _forum.EndChatForUser(Context.ConnectionId);
    }

    [HubMethodName(LoginFunctionName)]
    public async Task Login(string? username)
    {
        _logger.LogInformation("Login for user {Username}", username);
        if (string.IsNullOrWhiteSpace(username))
        {
            return;
        }

        if (IsSupport(username))
        {
            _forum.AddSupport(Context.ConnectionId, username);
        }
        else
        {
            _forum.AddUser(Context.ConnectionId, username);
        }
    }

    private static bool IsSupport(string username)
    {
        return username.StartsWith("support", StringComparison.InvariantCultureIgnoreCase);
    }


    [HubMethodName(PublishMessageMethodName)]
    public async Task PublishMessage(string? message, Guid? requestId)
    {
        if (message is null)
        {
            _logger.LogWarning("Published message content is null");
            return;
        }

        var chatId = _forum.FindGroupIdByConnectionId(Context.ConnectionId);
        if (chatId is null)
        {
            _logger.LogWarning("Невозможно найти ChatId для соединения {ConnectionId}", Context.ConnectionId);
            return;
        }
        
        _logger.LogInformation("Сообщение для группы {ChatId}", chatId);
        
        var username = _forum.GetUsername(Context.ConnectionId);
        if (username is null)
        {
            _logger.LogWarning("Невозможно найти имя пользователя для подключения {ConnectionId}", Context.ConnectionId);
            return;
        }
        
        _logger.LogInformation("Received message from {Username} with requestId {RequestId}", username, requestId);
        await _bus.Publish(new MessagePublishedEvent
                           {
                               Username = username, 
                               Message = message,
                               RequestId = requestId
                           });
        _logger.LogInformation("Event published");
        await Clients.Group(chatId).SendAsync(PublishMessageMethodName, username, message, requestId?.ToString());
        _logger.LogInformation("Сообщение отправлено в чат");
    }

    protected override void Dispose(bool disposing)
    {
        _forum.ChatEnded -= ForumOnChatEnded;
        _forum.ChatStarted -= ForumOnChatStarted;
        base.Dispose(disposing);
    }
}