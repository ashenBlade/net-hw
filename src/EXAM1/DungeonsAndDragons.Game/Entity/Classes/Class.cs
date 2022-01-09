using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;
using DungeonsAndDragons.Game.Entity.Equipments.Weapons;

namespace DungeonsAndDragons.Game.Entity.Classes;

public abstract class Class
{
    public string Name { get; private set; }
    public Hits Hits { get; private set; }
    public ClassPossessions ClassPossessions { get; private set; }
    
    protected Class(Hits hits, string name, ClassPossessions classPossessions)
    {
        Hits = hits;
        Name = name;
        ClassPossessions = classPossessions;
    }
}
