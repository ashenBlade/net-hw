using TicTacToe.Web.Models;

namespace TicTacToe.Web.Managers;

public interface IGameManager
{
    Game CreateGame(User owner, int maxRank);
    void StartGame(int gameId);
    void MakeAMove(Game game, int x, int y, int turn);
    List<Game> GetGamesPaged(int pageNumber, int pageSize);
}