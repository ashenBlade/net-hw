// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HW13;

BenchmarkRunner.Run<DynamicTest>();

public class DynamicTest : TestBase
{
    [Benchmark(Description = "Dynamic sum")]
    public override void Sum()
    {
        Invoker.SumDynamic();
    }
}