// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HW13;

BenchmarkRunner.Run<GenericTest>();

class GenericTest : TestBase
{
    public GenericTest()
    {
        SumMethod = t => Invoker.SumGeneric<int>(t);
    }

    protected override SumDelegate SumMethod { get; }
}