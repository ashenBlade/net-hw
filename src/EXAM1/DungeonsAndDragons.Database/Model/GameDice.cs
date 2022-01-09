using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DungeonsAndDragons.Database.Model;

[Owned]
public class GameDice
{
    [Required]
    [Range(1, 100)]
    public int MaxValue { get; set; }
    [Required]
    [Range(1, 100)]
    public int DiceAmount { get; set; }
}