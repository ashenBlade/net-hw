using Microsoft.AspNetCore.SignalR;

namespace MessagesAPI.Models;

public record ConnectedUser(string Username, string UserId)
{
    public override int GetHashCode()
    {
        return Username.GetHashCode();
    }
}