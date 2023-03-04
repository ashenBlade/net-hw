using ChatService;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace GrpcChat.Chat.Web.Services;

[Authorize]
public class GrpcChatService: ChatService.ChatService.ChatServiceBase
{
    private static readonly Empty EmptyResponse = new Empty();
    
    private readonly ILogger<GrpcChatService> _logger;

    public GrpcChatService(ILogger<GrpcChatService> logger)
    {
        _logger = logger;
    }

    public override async Task<Empty> SendMessage(SendMessageRequest request, ServerCallContext context)
    {
        return EmptyResponse;
    }

    public override async Task GetChatMessages(Empty request, IServerStreamWriter<ChatMessageResponse> responseStream, ServerCallContext context)
    {
        // Ничего
    }
}