// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HW13;

new DynamicTest().Run();

public class DynamicTest : TestBase
{
    [Benchmark(Description = "Dynamic sum")]
    public override void Sum()
    {
        Invoker.SumDynamic();
    }
}