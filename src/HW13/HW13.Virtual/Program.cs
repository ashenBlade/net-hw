// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HW13;

BenchmarkRunner.Run<VirtualTests>();

public class VirtualTests : TestBase
{
    public VirtualTests()
    {
        Invoker = new SumMethodsOverride();
    }


    [Benchmark(Description = "Virtual sum")]
    public override void Sum()
    {
        Invoker.SumVirtual();
    }
}