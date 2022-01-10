using System.ComponentModel.DataAnnotations;
using DungeonsAndDragons.Shared.Models;


namespace DungeonsAndDragons.Database.Model;

public class Monster
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MinLength(3)]
    public string Name { get; set; }
    [Required]
    [Range(1, 1000)]
    public int HitPoints { get; set; }
    [Required]
    [Range(1, 100)]
    public int AttackModifier { get; set; }
    [Required]
    [Range(1, 100)]
    public int AttackPerRound { get; set; }
    [Required]
    [Range(1, 100)]
    public int Damage { get; set; }
    [Required]
    public int DamageModifier { get; set; }
    [Required]
    [Range(1, 100)]
    public int Weapon { get; set; }
    [Required]
    [Range(1, 100)]
    public int ArmorClass { get; set; }

    public static Entity ToSharedEntity(Monster monster)
    {
        return new Entity()
               {
                   Name = monster.Name,
                   HitPoints = monster.HitPoints,
                   ArmorClass = monster.ArmorClass,
                   AttackModifier = monster.AttackModifier,
                   AttackPerRound = monster.AttackPerRound,
                   Damage = monster.Damage,
                   DamageModifier = monster.DamageModifier,
                   Weapon = monster.Weapon
               };
    }
}