namespace HW13;

public class SumMethods
{
    public int Sum(int times)
    {
        var sum = 0;
        for (int i = 0; i < times; i++)
        {
            sum = unchecked( sum + i );
        }

        return sum;
    }

    public virtual int SumVirtual(int times)
    {
        return times;
    }

    public TReturn SumGeneric<TReturn>(int times)
    {
        var sum = 0;
        for (int i = 0; i < times; i++)
        {
            sum = unchecked( sum + i );
        }

        return default;
    }

    public static int SumStatic(int times)
    {
        var sum = 0;
        for (int i = 0; i < times; i++)
        {
            sum = unchecked( sum + i );
        }

        return sum;
    }

    public int SumDynamic(int times)
    {
        dynamic sum = 0;
        for (int i = 0; i < times; i++)
        {
            sum = unchecked( sum + i );
        }

        return sum;
    }

    public int SumReflection(int times)
    {
        return ( int ) typeof(SumMethods).GetMethod(nameof(Sum))!.Invoke(this, new object[] {times})!;
    }
}