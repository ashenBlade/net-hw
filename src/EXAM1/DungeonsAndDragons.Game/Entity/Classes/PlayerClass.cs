using DungeonsAndDragons.Game.Common;

namespace DungeonsAndDragons.Game.Entity.Classes;

public class PlayerClass : Class
{
    public PlayerClass(Hits hits, string name, ClassPossessions classPossessions) 
        : base(hits, name, classPossessions) { }
}