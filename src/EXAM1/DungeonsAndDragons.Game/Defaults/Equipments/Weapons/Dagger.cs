using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Defaults.Equipments.Weapons;

public class Dagger : SimpleMeleeWeapon
{
    public Dagger() 
        : base("Dagger", 
               WeaponProperty.Light, 
               DamageType.Bludgeoning, 
               new GameDice(1, 4)) 
    { }
}