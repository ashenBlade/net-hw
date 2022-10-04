using Microsoft.AspNetCore.SignalR;

namespace MessagesAPI;

public class ChatHub : Hub
{
    [EndpointName("publishMessage")]
    public async Task PublishMessage(string username, string message)
    {
        await this.Clients.All.SendAsync("sendMessage",username, message);
    }
}