using Microsoft.EntityFrameworkCore;

namespace DungeonsAndDragons.Database.Model;

[Owned]
public class GameDice
{
    public int MaxValue { get; set; }
    public int DiceAmount { get; set; }
}