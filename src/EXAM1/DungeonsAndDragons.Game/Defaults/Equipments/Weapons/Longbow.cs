using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class Longbow : MartialRangedWeapon
{
    public Longbow()
    : base("Longbow", WeaponProperty.TwoHanded, DamageType.Piercing, new GameDice(8, 1))
    { }
}