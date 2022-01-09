using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class Mace : SimpleMeleeWeapon
{
    public Mace()
    : base("Mace", WeaponProperty.None, DamageType.Bludgeoning, new GameDice(6, 1))
    {
        
    }
}