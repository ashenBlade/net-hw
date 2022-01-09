namespace DungeonsAndDragons.Game.Common;

public class Hits
{
    private readonly GameDice _dice;
    private readonly int _defaultHitIncreaseAmount;
    public int MaxHits { get; private set; }
    public int CurrentHits { get; private set; }
    public int CurrentLevel { get; private set; }
    public Hits(GameDice dice, int maxHits, int defaultHitIncreaseAmount)
    {
        if (maxHits < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(maxHits), maxHits, "Max hits points must be positive");
        }

        if (defaultHitIncreaseAmount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(defaultHitIncreaseAmount), defaultHitIncreaseAmount,
                                                  "Default hit increase amount must be non-negative");
        }
        _dice = dice;
        _defaultHitIncreaseAmount = defaultHitIncreaseAmount;
        CurrentHits = MaxHits = maxHits;
        CurrentLevel = 1;
    }
    
    /// <returns>Added amount of hit points</returns>
    public int LevelUp(int constitutionModifier, bool useDefault = false)
    {
        var toAdd = ( useDefault
                          ? _defaultHitIncreaseAmount
                          : _dice.Roll() )
                  + constitutionModifier;
        toAdd = toAdd < 0
                    ? 0
                    : toAdd;
        MaxHits += toAdd;
        CurrentLevel++;
        return toAdd;
    }

    public void Hit(int amount)
    {
        var newValue = CurrentHits - amount;
        CurrentHits = newValue < 0
                          ? 0
                          : newValue;
    }

    public void Heal(int amount)
    {
        var newValue = CurrentHits + amount;
        CurrentHits = newValue > MaxHits
                          ? MaxHits
                          : newValue;
    }
}