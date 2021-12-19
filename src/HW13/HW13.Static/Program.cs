// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Running;
using HW13;

BenchmarkRunner.Run<StaticTests>();

class StaticTests : TestBase
{
    public StaticTests()
    {
        SumMethod = SumMethods.SumStatic;
    }

    protected override SumDelegate SumMethod { get; }

}