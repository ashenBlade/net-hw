using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;

namespace HW13;

public delegate int SumDelegate(int times);

[MemoryDiagnoser]
[MinColumn]
[MaxColumn]
[MedianColumn]
[MeanColumn]
[StdDevColumn]
public abstract class TestBase
{
    protected SumMethods Invoker { get; set; } = new();
    protected abstract SumDelegate SumMethod { get;  }

    [Benchmark]
    public void Sum()
    {
        SumMethod(SumMethods.DefaultTimesCount);
    }
}