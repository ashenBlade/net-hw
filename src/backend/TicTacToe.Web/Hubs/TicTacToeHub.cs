using System.Collections.Concurrent;
using System.Globalization;
using System.Security.Claims;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TicTacToe.Events;
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
        _logger.LogInformation("{Value}", Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var game = await _gameRepository.FindGameByUserIdAsync(UserId);
        _logger.LogWarning(exception, "Пользователь отключился");
        if (game is not null)
        {
            game.ForceEndGame(game.OwnerId == UserId ? game.Owner : game.SecondPlayer!);
            await _gameRepository.UpdateGameAsync(game);
            if (UserIdToConnectionId.TryRemove(game.OwnerId.ToString(), out var connId))
            {
                await Clients.Client(connId).SendAsync("GameEnded");
            }
            if (UserIdToConnectionId.TryRemove(game.SecondPlayerId?.ToString() ?? string.Empty, out connId))
            {
                await Clients.Client(connId).SendAsync("GameEnded");
            }

            await _bus.Publish(new GameEndedEvent()
                               {
                                   OwnerId = game.OwnerId,
                                   SecondPlayerId = game.SecondPlayerId!.Value,
                                   GameId = game.Id,
                                   OwnerRank = game.Owner.Rank,
                                   SecondPlayerRank = game.SecondPlayer!.Rank
                               });
        }
        await base.OnDisconnectedAsync(exception);
    }


    private int UserId => int.Parse( Context.User.FindFirstValue(ClaimTypes.NameIdentifier) );
    private string UserIdString => Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

    [HubMethodName("ConnectToGame")]
    public async Task ConnectToGameAsync(string gameIdString)
    {
        try
        {
            var gameId = int.Parse(gameIdString);

            _logger.LogInformation("Получен запрос на присоединение к игре {Id}", gameId);
            var game = await _gameRepository.FindActiveGameByIdAsync(gameId);
            if (game is null)
            {
                throw new Exception("Не найдено");
            }

            _logger.LogInformation("Нахожу пользователя по ID {Id}", UserIdString);
            var user = await _userManager.FindByIdAsync(UserIdString);
            _logger.LogInformation("Обновляю данные об игре, Статус = {Status}", game.Status switch
                                                                                 {
                                                                                     GameStatus.Created => "Created",
                                                                                     GameStatus.Ended   => "Ended",
                                                                                     GameStatus.Playing => "Playing",
                                                                                     _                  => "Блять"
                                                                                 });
            game.Start(user);
            _logger.LogInformation("Обновляю данные об игре, Статус = {Status}", game.Status switch
                                                                                 {
                                                                                     GameStatus.Created => "Created",
                                                                                     GameStatus.Ended => "Ended",
                                                                                     GameStatus.Playing => "Playing",
                                                                                     _ => "Блять"
                                                                                 });
            await _gameRepository.UpdateGameAsync(game);
            if (game.OwnerId is not 0 && game.SecondPlayerId is not null )
            {
                _logger.LogInformation("Посылаю информацию о начале игры");
                var startDate = game.StartDate.ToString(CultureInfo.InvariantCulture);
                await Task.WhenAll( FindClient(game.OwnerId)
                                       .SendAsync("GameStarted", game.Id, game.SecondPlayer?.UserName ?? "Противник", game.OwnerSign == GameSign.O ? "O" : "X",
                                                  startDate),
                                    FindClient(game.SecondPlayerId!.Value)
                                       .SendAsync("GameStarted", game.Id, game.Owner?.UserName ?? "Противник", game.SecondPlayerSign  == GameSign.O ? "O" : "X",
                                                  startDate) );
            }
            else
            {
                _logger.LogError("Пользователя нет в кеше");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка во время присоединения к игре");
        }
    }

    private IClientProxy FindClient(int userId)
    {
        return Clients.Client(UserIdToConnectionId[userId.ToString()]);
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
        _logger.LogInformation("Игра: {Status}", game.Status);
        _logger.LogInformation("Делается ход для игры {Id} в {X}-{Y} знаком {Sign}", game.Id, x, y, currentSign);
        var stepResult = game.MakeStep(x, y);
        _logger.LogInformation("Результат хода. End {End}, IsDraw {IsDraw}", stepResult.GameEnded, stepResult.IsDraw);
        await _gameRepository.UpdateGameAsync(game);
        var gameClients = GetGameClients(game);
        await gameClients.SendAsync("MakeStep", x, y, currentSign == GameSign.O
                                                          ? "O"
                                                          : "X");
        if (stepResult.GameEnded)
        {
            await
                Task.WhenAll(FindClient(game.OwnerId)
                                .SendAsync("GameEnded", stepResult.OwnerPoints, stepResult.SecondPlayerPoints),
                             FindClient(game.SecondPlayerId!.Value)
                                .SendAsync("GameEnded", stepResult.SecondPlayerPoints, stepResult.OwnerPoints));
            await _bus.Publish(new GameEndedEvent()
                               {
                                   OwnerId = game.OwnerId,
                                   SecondPlayerId = game.SecondPlayerId!.Value,
                                   GameId = game.Id,
                                   OwnerRank = game.Owner.Rank,
                                   SecondPlayerRank = game.SecondPlayer!.Rank
                               });
        }
    }

    private IClientProxy GetGameClients(Game game)
    {
        return Clients.Clients(UserIdToConnectionId[game.OwnerId.ToString()],
                               UserIdToConnectionId[game.SecondPlayerId.ToString()!]);
    }

    private Task<Game?> GetGameForCurrentUserAsync() => _gameRepository.FindActiveGameByIdAsync(UserId);  

    [HubMethodName("EndGame")]
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
        await _bus.Publish(new GameEndedEvent()
                           {
                               OwnerId = game.OwnerId,
                               SecondPlayerId = game.SecondPlayerId!.Value,
                               GameId = game.Id,
                               OwnerRank = game.Owner.Rank,
                               SecondPlayerRank = game.SecondPlayer!.Rank
                           });
    }
}