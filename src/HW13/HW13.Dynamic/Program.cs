// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using HW13;

BenchmarkRunner.Run<DynamicTest>();

class DynamicTest : TestBase
{
    public DynamicTest()
    {
        SumMethod = t => Invoker.SumDynamic(t);
    }

    protected override SumDelegate SumMethod { get; }
}