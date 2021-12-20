// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HW13;

new StandardTests().Run();
public class StandardTests : TestBase
{
    [Benchmark(Description = "Standard sum")]
    public override void Sum()
    {
        Invoker.Sum();
    }
}