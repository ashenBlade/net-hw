namespace TicTacToe.Web.Models;

public static class GameSignExtensions
{
    public static GameSign GetAlternateSign(this GameSign sign) =>
        sign switch
        {
            GameSign.O => GameSign.X,
            GameSign.X => GameSign.O,
            _          => GameSign.None
        };
}