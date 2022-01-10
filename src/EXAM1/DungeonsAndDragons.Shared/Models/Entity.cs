using System.ComponentModel.DataAnnotations;

namespace DungeonsAndDragons.Shared.Models;

public class Entity
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int HitPoints { get; set; }
    [Required]
    public int AttackModifier { get; set; }
    [Required]
    public int AttackPerRound { get; set; }
    [Required]
    public int DamageCount { get; set; }
    [Required]
    public int DamageMax { get; set; }
    [Required]
    public int DamageModifier { get; set; }
    [Required]
    public int Weapon { get; set; }
    [Required]
    public int ArmorClass { get; set; }
}