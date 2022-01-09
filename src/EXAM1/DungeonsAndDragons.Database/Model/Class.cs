using System.Collections;
using System.ComponentModel.DataAnnotations;
using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;
using Microsoft.EntityFrameworkCore;

namespace DungeonsAndDragons.Database.Model;

public class Class
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public GameDice HitsGameDice { get; set; }
}