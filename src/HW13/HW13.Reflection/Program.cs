// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HW13;

new ReflectionTests().Run();

public class ReflectionTests : TestBase
{
    [Benchmark(Description = "Reflection sum")]
    public override void Sum()
    {
        Invoker.SumReflection();
    }
}