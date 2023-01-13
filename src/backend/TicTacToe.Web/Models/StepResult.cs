namespace TicTacToe.Web.Models;

public struct StepResult
{
    public User? Winner { get; set; }

    public bool HasWinner => Winner is not null;
}