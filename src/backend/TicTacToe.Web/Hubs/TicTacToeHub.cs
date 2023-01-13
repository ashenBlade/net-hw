using Microsoft.AspNetCore.SignalR;

namespace TicTacToe.Web.Hubs;

public class TicTacToeHub: Hub
{
    private readonly ILogger<TicTacToeHub> _logger;
    
    public TicTacToeHub(ILogger<TicTacToeHub> logger)
    {
        _logger = logger;
    }
}