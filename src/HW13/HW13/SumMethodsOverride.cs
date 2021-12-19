namespace HW13;

public class SumMethodsOverride : SumMethods
{
    public override int SumVirtual(int times = DefaultTimesCount)
    {
        var sum = 0;
        for (int i = 0; i < times; i++)
        {
            sum = checked( sum + i );
        }

        return sum;
    }
}