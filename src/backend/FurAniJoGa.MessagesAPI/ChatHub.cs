using Microsoft.AspNetCore.SignalR;

namespace MessagesAPI;

public class ChatHub : Hub
{
    public async Task PublishMessage(string username, string message)
    {
        await this.Clients.All.SendAsync("sendMessage",username, message);
    }
}