using DungeonsAndDragons.Game.Defaults.Classes.MonsterClasses;
using DungeonsAndDragons.Game.Defaults.Races;
using DungeonsAndDragons.Game.Entity.Characteristics;
using DungeonsAndDragons.Game.Entity.Classes;
using DungeonsAndDragons.Game.Entity.Creatures;
using DungeonsAndDragons.Game.Entity.Ideology;
using DungeonsAndDragons.Game.Entity.Races;

namespace DungeonsAndDragons.Game.Defaults.Monsters;

public class Aarakocra : Monster
{
    public Aarakocra() 
        : base("Aarakocra", 
               new Humanoid(), 
               new Human(), 
               new CharacteristicsSet(10, 14, 10, 11, 12, 11), 
               IdeologyType.Neutral) { }
}