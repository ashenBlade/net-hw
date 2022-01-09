using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class Weaponless : Weapon
{
    public Weaponless()
    : base("None",
           WeaponProperty.None,
           WeaponProficiency.Simple,
           WeaponRangeType.Melee,
           DamageType.Slashing,
           new GameDice(8, 1))
    { }
}