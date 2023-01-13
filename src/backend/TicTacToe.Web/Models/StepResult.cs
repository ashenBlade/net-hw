namespace TicTacToe.Web.Models;

public struct StepResult
{
    public bool GameEnded => IsDraw || HasWinner;
    public bool IsDraw { get; set; }
    public User? Winner { get; set; }

    public bool HasWinner => Winner is not null;
    public int OwnerPoints { get; set; }
    public int SecondPlayerPoints { get; set; }
}