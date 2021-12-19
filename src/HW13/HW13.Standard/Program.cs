// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Running;
using HW13;

BenchmarkRunner.Run<StandardTests>();

class StandardTests : TestBase
{
    public StandardTests()
    {
        SumMethod = t => Invoker.Sum(t);
    }

    protected override SumDelegate SumMethod { get; }
}