// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HW13;

new GenericTest().Run();

public class GenericTest : TestBase
{
    public GenericTest()
    {
    }

    [Benchmark(Description = "Sum generic")]
    public override void Sum()
    {
        Invoker.SumGeneric<int>();
    }
}