using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class Rapier : MartialMeleeWeapon
{
    public Rapier()
    : base("Rapier", WeaponProperty.Fitnesse, DamageType.Piercing, new GameDice(8, 1))
    {
        
    }
}