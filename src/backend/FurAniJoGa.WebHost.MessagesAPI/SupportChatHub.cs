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

    private async void ForumOnChatStarted(ChatEventArgs args)
    {
        var (chatId, user, support) = args;
        _logger.LogInformation("Начался чат {ChatId}", chatId);
        await Task.WhenAll(Groups.AddToGroupAsync(user, chatId),
                           Groups.AddToGroupAsync(support, chatId));
        await Task.WhenAll(Clients.Client(user).SendAsync(OnChatStartedFunctionName, "user"),
                           Clients.Client(support).SendAsync(OnChatStartedFunctionName, "support"));
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _forum.DisconnectUser(Context.ConnectionId);
    }

    private async void ForumOnChatEnded(ChatEventArgs args)
    {
        var (chatId, user, support) = args;
        _logger.LogInformation("Чат {ChatId} заканчивается", chatId);
        await Clients.Group(chatId).SendAsync(OnChatEndedFunctionName);
        await Task.WhenAll(Groups.RemoveFromGroupAsync(user, chatId),
                           Groups.RemoveFromGroupAsync(support, chatId));
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
            return;
        }
        
        var username = _forum.GetUsername(Context.ConnectionId);
        if (username is null)
        {
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
        await Clients.Group(chatId).SendAsync(PublishMessageMethodName, username, message, requestId.ToString());
    }

    protected override void Dispose(bool disposing)
    {
        _forum.ChatEnded -= ForumOnChatEnded;
        _forum.ChatStarted -= ForumOnChatStarted;
        base.Dispose(disposing);
    }
}