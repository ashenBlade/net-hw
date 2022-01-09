using DungeonsAndDragons.Game.Entity.Characteristics;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;
using DungeonsAndDragons.Game.Entity.Ideology;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Entity.Creatures;

public abstract class Creature
{
    public string Name { get; private set; }
    public Class Class { get; private set; }
    public Race Race { get; private set; }
    public CharacteristicsSet Characteristics { get; private set; }
    public IdeologyType Ideology { get; private set; }
    public ICollection<Equipments.Equipment> Inventory { get; private set; } = new List<Equipments.Equipment>();
    public Weapon Weapon { get; private set; }
    public Armor Armor { get; private set; }
    
    protected Creature(string name, 
                       Class @class, 
                       Race race, 
                       CharacteristicsSet characteristics, 
                       IdeologyType ideology,
                       Weapon weapon,
                       Armor armor)
    {
        Name = name;
        Class = @class;
        Race = race;
        Characteristics = characteristics;
        Ideology = ideology;
        Weapon = weapon;
        Armor = armor;
    }
}
