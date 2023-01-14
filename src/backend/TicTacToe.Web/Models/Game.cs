using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Web.Models;

public class Game
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public User Owner { get; set; } = null!;

    public GameSign OwnerSign => GameSign.X;
    public GameSign SecondPlayerSign => OwnerSign.GetAlternateSign();

    public int? SecondPlayerId { get; set; }
    [ForeignKey(nameof(SecondPlayerId))]
    public User? SecondPlayer { get; set; }

    [Required]
    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    [Required]
    public GameSign CurrentSign { get; set; } = GameSign.O;
    
    [Required]
    public GameStatus Status { get; set; }
    
    [Required]
    public int MaxRank { get; set; }

    public GameSign[] Field { get; set; } = new[]
                                            {
                                                GameSign.None, GameSign.None, GameSign.None, 
                                                GameSign.None, GameSign.None, GameSign.None,
                                                GameSign.None, GameSign.None, GameSign.None
                                            };

    private int GetFieldIndex(int x, int y) => ( x - 1 ) * 3 + (y - 1);
    private (int x, int y) ToUserIndex(int index) => ( index / 3 + 1, index % 3 + 1 );

    public StepResult MakeStep(int x, int y)
    {
        if (Status == GameStatus.Ended)
        {
            throw new InvalidOperationException("Игра закончена");
        }
        
        var index = GetFieldIndex(x, y);
        if (Field[index] != GameSign.None)
        {
            throw new Exception("Ход уже был сделан");
        }

        Field[index] = CurrentSign;
        CurrentSign = CurrentSign.GetAlternateSign();
        var winner = CheckWinner();
        if (winner is not null)
        {
            var looser = winner == Owner
                             ? SecondPlayer!
                             : Owner;
            looser.Rank -= 1;
            winner.Rank += 3;
            Status = GameStatus.Ended;
        }

        if (IsDraw())
        {
            Status = GameStatus.Ended;
        }
        return new StepResult() {Winner = winner};
    }

    public bool IsDraw()
    {
        return Field.All(x => x != GameSign.None);
    }

    public User? CheckWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            var sign = Field[i];
            if (Field[i] == Field[i + 3] && Field[i + 3] == Field[i + 6])
            {
                return sign == OwnerSign
                           ? Owner
                           : SecondPlayer;
            }
        }

        for (int i = 0; i < 7; i+=3)
        {
            if (Field[i] == Field[i + 1] && Field[i + 1] == Field[i + 2])
            {
                var sign = Field[i];
                return sign == OwnerSign
                           ? Owner
                           : SecondPlayer;
            }
        }

        if (Field[0] == Field[4] && Field[4] == Field[8])
        {
            var sign = Field[0];
            return sign == OwnerSign
                       ? Owner
                       : SecondPlayer;
        }
        
        if (Field[2] == Field[4] && Field[4] == Field[6])
        {
            var sign = Field[0];
            return sign == OwnerSign
                       ? Owner
                       : SecondPlayer;
        }

        return null;
    }

    public void Start(User opponent)
    {
        if (Status != GameStatus.Created)
        {
            throw new InvalidOperationException("Нельзя присоединиться к запущеной игре");
        }

        if (SecondPlayer is not null)
        {
            throw new InvalidOperationException("В игре уже присутстсует 2 пользователь");
        }

        if (opponent.Id == OwnerId)
        {
            throw new InvalidOperationException("Владелец уже в игре");
        }

        SecondPlayer = opponent;
        Status = GameStatus.Playing;
    }

    public void ForceEndGame(User quited)
    {
        User quit, other;
        if (quited == Owner)
        {
            
        }
        Status = GameStatus.Ended;
    }
}