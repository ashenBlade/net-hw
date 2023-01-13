using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations.Rules;
using TicTacToe.Web.Models;

namespace TicTacToe.Web.Managers;

public class GameManager : IGameManager
{
    private readonly TicTacToeDbContext _context;

    public GameManager(TicTacToeDbContext context)
    {
        _context = context;
    }
    
    public Game CreateGame(User owner, int maxRank)
    {
        return new Game(owner, maxRank);
    }

    public async void StartGame(int gameId)
    {
        var game = await _context.Games.SingleOrDefaultAsync(g => g.Id == gameId);
        if (game != null) game.IsStarted = true;
    }

    public void MakeAMove(Game game, int x, int y, int turn)
    {
        game.Xs[x] = turn == 1 ? 1 : 2;
        game.Ys[y] = turn == 1 ? 1 : 2;
    }

    public User GetWinnerIfEnded(Game game)
    {
        if (game.Xs[0] == game.Xs[1] && game.Xs[0] == game.Xs[2])
            return game.Xs[0] == 1 ? game.Owner : game.SecondPlayer;
        if (game.Ys[0] == game.Ys[1] && game.Ys[0] == game.Ys[2])
            return game.Ys[0] == 1 ? game.Owner : game.SecondPlayer;
        if (game.Xs[0] == game.Ys[0] && game.Xs[1] == game.Ys[1] && game.Xs[2] == game.Ys[2])
            return game.Xs[0] == 1 ? game.Owner : game.SecondPlayer;
        if (game.Xs[1] == game.Ys[1] && game.Xs[0] == game.Ys[2] && game.Xs[2] == game.Ys[0])
            return game.Xs[1] == 1 ? game.Owner : game.SecondPlayer;
        return null;
    }

    public List<Game> GetGamesPaged(int pageNumber, int pageSize)
    {
        return _context.Games
            .OrderBy(g => g.IsStarted)
            .ThenBy(g => g.StartDate)
            .Skip(( pageNumber - 1 ) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}