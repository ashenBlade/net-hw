using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Web.Models;

public class Game
{
    public Game(User owner, int maxRank)
    {
        Owner = owner;
        MaxRank = maxRank;
        StartDate = DateTime.UtcNow;
    }
    
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public User Owner { get; set; }
    [Required]
    public User SecondPlayer { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public bool IsStarted { get; set; }
    
    [Required]
    public int MaxRank { get; set; }
    
    public int[] Xs { get; set; }
    public int[] Ys { get; set; }
}