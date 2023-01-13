using TicTacToe.Web.Models;

namespace TicTacToe.Web.ViewModels;

public class GameCreationViewModel
{
    public User Owner { get; set; }
    public int MaxRank { get; set; }
}