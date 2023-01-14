namespace TicTacToe.Events;

public class GameEndedEvent
{
    public int GameId { get; set; }
    public int OwnerId { get; set; }
    public int OwnerRank { get; set; }
    public int SecondPlayerId { get; set; }
    public int SecondPlayerRank { get; set; }
}