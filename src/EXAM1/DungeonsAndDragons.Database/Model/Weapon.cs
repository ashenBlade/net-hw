using System.ComponentModel.DataAnnotations;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Database.Model;

public class Weapon
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DamageType DamageType { get; set; }
    [Required]
    public WeaponRangeType RangeType { get; set; }
    [Required]
    public WeaponProficiency Proficiency { get; set; }
    [Required]
    public WeaponProperty Property { get; set; }
    [Required]
    public GameDice GameDice { get; set; }
}