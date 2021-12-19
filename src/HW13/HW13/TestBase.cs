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
    protected SumMethods Invoker { get; init; } = new();

    [Benchmark]
    public abstract void Sum();
}