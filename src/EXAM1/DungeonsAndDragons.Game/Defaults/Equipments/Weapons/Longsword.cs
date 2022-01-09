using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class Longsword : MartialMeleeWeapon
{
    public Longsword()
    : base("Longsword", WeaponProperty.Light, DamageType.Piercing, new GameDice(8, 1))
    {
        
    }
}