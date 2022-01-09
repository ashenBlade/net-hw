using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class Crossbow : SimpleRangedWeapon
{
    public Crossbow() 
        : base("Crossbow", WeaponProperty.Loading, DamageType.Piercing, new GameDice(8, 1)) 
    { }
}