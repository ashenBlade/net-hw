using TicTacToe.Web.Models;

namespace TicTacToe.Web.GameRepository;

public interface IGameRepository
{
    Task<List<Game>> GetGamesPagedAsync(int page, int size, CancellationToken token = default);
    Task<Game> CreateGameAsync(int ownerId, int rank);
}