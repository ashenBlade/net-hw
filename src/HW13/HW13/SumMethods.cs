namespace HW13;

public class SumMethods
{
    public const int DefaultTimesCount = 10000;
    public int Sum(int times = DefaultTimesCount)
    {
        var sum = 0;
        for (int i = 0; i < times; i++)
        {
            sum = unchecked( sum + i );
        }

        return sum;
    }

    public virtual int SumVirtual(int times = DefaultTimesCount)
    {
        return times;
    }

    public TReturn SumGeneric<TReturn>(int times = DefaultTimesCount)
    {
        var sum = 0;
        for (int i = 0; i < times; i++)
        {
            sum = unchecked( sum + i );
        }

        return default;
    }

    public static int SumStatic(int times = DefaultTimesCount)
    {
        var sum = 0;
        for (int i = 0; i < times; i++)
        {
            sum = unchecked( sum + i );
        }

        return sum;
    }

    public int SumDynamic(int times = DefaultTimesCount)
    {
        dynamic sum = 0;
        for (int i = 0; i < times; i++)
        {
            sum = unchecked( sum + i );
        }

        return sum;
    }

    public int SumReflection(int times = DefaultTimesCount)
    {
        return ( int ) typeof(SumMethods).GetMethod(nameof(Sum))!.Invoke(this, new object[] {times})!;
    }
}