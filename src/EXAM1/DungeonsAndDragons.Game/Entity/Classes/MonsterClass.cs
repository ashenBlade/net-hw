using DungeonsAndDragons.Game.Common;

namespace DungeonsAndDragons.Game.Entity.Classes;

public class MonsterClass : Class
{
    public MonsterClass(Hits hits, string name, ClassPossessions classPossessions) 
        : base(hits, name, classPossessions) { }
}