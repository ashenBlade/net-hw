using System.ComponentModel.DataAnnotations;
using BenchmarkDotNet.Attributes;

namespace HW13;

[MemoryDiagnoser]
[MinColumn]
[MaxColumn]
[MedianColumn]
[MeanColumn]
[StdDevColumn]
public class Tests
{
    private SumMethods _methods;
    private SumMethodsOverride _override;
    [GlobalSetup]
    public void SetUp()
    {
        _methods = new SumMethods();
        _override = new SumMethodsOverride();
    }

    [Params(1, 5, 10, 50, 100)]
    public int Times { get; set; }

    [Benchmark(Description = "Sum")]
    public void Sum()
    {
        _methods.Sum(Times);
    }

    [Benchmark(Description = "Sum virtual override")]
    public void SumOverride()
    {
        _override.SumVirtual(Times);
    }

    [Benchmark(Description = "Sum static")]
    public void SumStatic()
    {
        SumMethods.SumStatic(Times);
    }

    [Benchmark(Description = "Sum generic")]
    public void SumGeneric()
    {
        _methods.SumGeneric<double>(Times);
    }

    [Benchmark(Description = "Sum using reflection")]
    public void SumReflection()
    {
        _methods.SumReflection(Times);
    }

    [Benchmark(Description = "Sum with dynamic")]
    public void SumDynamic()
    {
        _methods.SumDynamic(Times);
    }
}