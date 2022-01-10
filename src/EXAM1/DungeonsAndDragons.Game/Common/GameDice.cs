namespace DungeonsAndDragons.Game.Common;

public class GameDice
{
    public int MaxValue { get; private set; }
    public int DiceAmount { get; private set; }
    private readonly Random _random;
    public GameDice(int maxValue, int diceAmount)
    {
        if (maxValue < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(maxValue), maxValue,
                                                  "Max value of game dice must be positive");
        }
        if (diceAmount < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(diceAmount), diceAmount, "Amount of dices must be positive");
        }
        MaxValue = maxValue;
        DiceAmount = diceAmount;
        _random = new Random();
    }

    public int Roll(int attackModifier = 0)
    {
        return _random.Next(DiceAmount * attackModifier, (MaxValue + attackModifier) * DiceAmount);
    }
}