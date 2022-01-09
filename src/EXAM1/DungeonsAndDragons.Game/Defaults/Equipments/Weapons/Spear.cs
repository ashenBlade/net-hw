using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class Spear : SimpleMeleeWeapon
{
    public Spear() 
        : base("Spear", WeaponProperty.Thrown, DamageType.Piercing, new GameDice(6, 1)) 
    { }
}