using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class Maul : MartialMeleeWeapon
{
    public Maul()
    : base("Maul", WeaponProperty.TwoHanded, DamageType.Bludgeoning, new GameDice(6, 2))
    { }
}