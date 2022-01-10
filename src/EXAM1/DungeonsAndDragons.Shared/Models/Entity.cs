namespace DungeonsAndDragons.Shared.Models;

public class Entity
{
    public string Name { get; set; }
    public int HitPoints { get; set; }
    public int AttackModifier { get; set; }
    public int AttackPerRound { get; set; }
    public int Damage { get; set; }
    public int DamageModifier { get; set; }
    public int Weapon { get; set; }
    public int ArmorClass { get; set; }
}