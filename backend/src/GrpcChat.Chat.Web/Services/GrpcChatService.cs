using System.Security.Claims;
using ChatService;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcChat.ChatService;
using Microsoft.AspNetCore.Authorization;

namespace GrpcChat.Chat.Web.Services;

[Authorize]
public class GrpcChatService: global::ChatService.ChatService.ChatServiceBase
{
    private static readonly Empty EmptyResponse = new();
    
    private readonly ILogger<GrpcChatService> _logger;
    private readonly IChatService _chatService;

    public GrpcChatService(IChatService chatService, ILogger<GrpcChatService> logger)
    {
        _logger = logger;
        _chatService = chatService;
    }

    public override async Task<Empty> SendMessage(SendMessageRequest request, ServerCallContext context)
    {
        var username = context.GetHttpContext().User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _chatService.SendMessageAsync(request.Message, username, context.CancellationToken);
        _logger.LogDebug("Пришло сообщение от пользователя {Username}", username);
        return EmptyResponse;
    }

    public override async Task GetChatMessages(Empty request, IServerStreamWriter<ChatMessageResponse> responseStream, ServerCallContext context)
    {
        
        var username = context.GetHttpContext().User.FindFirstValue(ClaimTypes.NameIdentifier);
        _logger.LogInformation("Пользователь {Username} начал получать все сообщения из чата", username);
        try
        {
            var receiver = _chatService.CreateMessageReceiver();
            var token = context.CancellationToken;
            await foreach (var message in receiver.ReadMessagesAsync(token))
            {
                await responseStream.WriteAsync(new ChatMessageResponse()
                                                {
                                                    Message = message.Message,
                                                    UserName = message.Username
                                                });
            }
        }
        catch (TaskCanceledException)
        { }
        
        _logger.LogInformation("Пользователь {Username} заканчивает получать сообщения из чата", username);
    }
}