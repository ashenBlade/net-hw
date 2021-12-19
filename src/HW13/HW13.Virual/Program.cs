// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Running;
using HW13;

BenchmarkRunner.Run<VirtualTests>();

class VirtualTests : TestBase
{
    public VirtualTests()
    {
        Invoker = new SumMethodsOverride();
        SumMethod = t => Invoker.SumVirtual(t);
    }

    protected override SumDelegate SumMethod { get; }
}