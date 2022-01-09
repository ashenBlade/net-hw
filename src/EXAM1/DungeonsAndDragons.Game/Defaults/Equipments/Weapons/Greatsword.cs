using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class Greatsword : MartialMeleeWeapon
{
    public Greatsword()
    : base("Greatsword", WeaponProperty.TwoHanded, DamageType.Slashing, new GameDice(6, 2))
    { }
}