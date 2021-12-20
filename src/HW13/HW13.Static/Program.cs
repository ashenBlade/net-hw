// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HW13;

new StaticTests().Run();

public class StaticTests : TestBase
{
    [Benchmark(Description = "Static sum")]
    public override void Sum()
    {
        SumMethods.SumStatic();
    }
}