using System.Diagnostics;

namespace DungeonsAndDragons.Game.Entity.Characteristics;

public class CharacteristicsSet
{
    public Characteristic Strength { get; private set; }
    public Characteristic Dexterity { get; private set; }
    public Characteristic Constitution { get; private set; }
    public Characteristic Intelligence { get; private set; }
    public Characteristic Wisdom { get; private set; }
    public Characteristic Charisma { get; private set; }

    public CharacteristicsSet(int strength, 
                              int dexterity, 
                              int constitution, 
                              int intelligence, 
                              int wisdom, 
                              int charisma)
    {
        Strength = new Strength(strength);
        Dexterity = new Dexterity(dexterity);
        Constitution = new Constitution(constitution);
        Intelligence = new Intelligence(intelligence);
        Wisdom = new Wisdom(wisdom);
        Charisma = new Charisma(charisma);
    }
}