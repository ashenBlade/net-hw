using System.Collections.Concurrent;
using System.Security.Claims;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TicTacToe.Web.GameRepository;
using TicTacToe.Web.Managers;
using TicTacToe.Web.Models;

namespace TicTacToe.Web.Hubs;

[Authorize]
public class TicTacToeHub: Hub
{
    private readonly IBus _bus;
    private readonly IGameRepository _gameRepository;
    private readonly TicTacUserManger _userManager;
    private readonly ILogger<TicTacToeHub> _logger;
    private static readonly ConcurrentDictionary<string, string> UserIdToConnectionId = new();

    public TicTacToeHub(IBus bus, IGameRepository gameRepository, TicTacUserManger userManager, ILogger<TicTacToeHub> logger)
    {
        _bus = bus;
        _gameRepository = gameRepository;
        _userManager = userManager;
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        UserIdToConnectionId[UserIdString] = Context.ConnectionId;
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var game = await _gameRepository.FindGameByUserIdAsync(UserId);
        if (game is not null)
        {
            if (UserIdToConnectionId.TryRemove(game.OwnerId.ToString(), out var connId))
            {
                await Clients.Client(connId).SendAsync("GameEnded");
            }
            if (UserIdToConnectionId.TryRemove(game.SecondPlayerId?.ToString() ?? string.Empty, out connId))
            {
                await Clients.Client(connId).SendAsync("GameEnded");
            }
            game.ForceEndGame(game.OwnerId == UserId ? game.Owner : game.SecondPlayer!);
            await _gameRepository.UpdateGameAsync(game);
        }
        await base.OnDisconnectedAsync(exception);
    }


    private int UserId => int.Parse( Context.User.FindFirstValue(ClaimTypes.NameIdentifier) );
    private string UserIdString => Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

    [HubMethodName("ConnectToGame")]
    public async Task ConnectToGameAsync(int gameId)
    {
        var game = await _gameRepository.FindActiveGameByIdAsync(gameId);
        if (game is null)
        {
            throw new Exception("Не найдено");
        }

        var user = await _userManager.FindByIdAsync(UserIdString);
        game.Start(user);
        await _gameRepository.UpdateGameAsync(game);
    }

    [HubMethodName("MakeStep")]
    public async Task MakeStepAsync(int x, int y)
    {
        var game = await _gameRepository.FindGameByUserIdAsync(UserId);
        if (game is null)
        {
            throw new Exception("Игры не нашлось");
        }

        var currentSign = game.CurrentSign;
        var stepResult = game.MakeStep(x, y);
        await _gameRepository.UpdateGameAsync(game);
        var gameClients = GetGameClients(game);
        await gameClients.SendAsync("MakeStep", x, y, currentSign == GameSign.O
                                                          ? "O"
                                                          : "X");
        if (stepResult.GameEnded)
        {
            await gameClients.SendAsync("GameEnded");
        }
    }

    private IClientProxy GetGameClients(Game game)
    {
        return Clients.Clients(UserIdToConnectionId[game.OwnerId.ToString()],
                               UserIdToConnectionId[game.SecondPlayerId.ToString()!]);
    }

    private Task<Game?> GetGameForCurrentUserAsync() => _gameRepository.FindActiveGameByIdAsync(UserId);  

    [HubMethodName("EndGameAsync")]
    public async Task EndGameAsync()
    {
        var game = await GetGameForCurrentUserAsync();
        if (game is null)
        {
            throw new Exception("Игры не найдено");
        }
        game.ForceEndGame(UserId == game.OwnerId ? game.Owner : game.SecondPlayer!);
        await _gameRepository.UpdateGameAsync(game);
        await Clients.Clients(UserIdToConnectionId[game.OwnerId.ToString()],
                              UserIdToConnectionId[game.SecondPlayerId.ToString()!])
                     .SendAsync("EndGame");
    }
}