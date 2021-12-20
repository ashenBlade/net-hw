using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;

namespace HW13;

[MemoryDiagnoser]
[MinColumn]
[MaxColumn]
[MedianColumn]
[MeanColumn]
[StdDevColumn]
public abstract class TestBase
{
    public const int RepeatTime = 20000;
    public const int IterationsCount = 1000;
    protected SumMethods Invoker { get; init; } = new();

    [Benchmark]
    public abstract void Sum();

    public void Run()
    {
        for (int i = 0; i < RepeatTime; i++)
        {
            for (int j = 0; j < IterationsCount; j++)
            {
                Sum();
            }
        }
    }
}