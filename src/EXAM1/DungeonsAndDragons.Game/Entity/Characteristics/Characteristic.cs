namespace DungeonsAndDragons.Game.Entity.Characteristics;

public class Characteristic
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public int Modificator => Score - ( Score > 10
                                            ? 10
                                            : 11 ) / 2;

    public Characteristic(string name, int score)
    {
        if (score is < 0 or > 30)
        {
            throw new ArgumentOutOfRangeException(nameof(score), score, "Score must be in range of [0; 30]");
        }
        Name = name;
        Score = score;
    }
}