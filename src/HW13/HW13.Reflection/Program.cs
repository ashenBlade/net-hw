// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HW13;

BenchmarkRunner.Run<ReflectionTests>();

public class ReflectionTests : TestBase
{
    [Benchmark(Description = "Reflection sum")]
    public override void Sum()
    {
        Invoker.SumReflection();
    }
}