using System;
using System.Net.Http;
using BenchmarkDotNet.Attributes;
using HW8;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApplication;

namespace HW12;

[MinColumn]
[MaxColumn]
[StdDevColumn]
[StdErrorColumn]
[MedianColumn]
public class CalculatorBenchmark : IDisposable
{
    private WebApplicationFactory<Startup> _cSharpFactory;
    private HttpClient _cSharpClient;
    private WebApplicationFactory<StartUp.StartUp> _fSharpFactory;
    private HttpClient _fSharpClient;

    private HttpRequestMessage CSharp1123Plus1221Message =>
        new(HttpMethod.Get, "https://localhost:5001/Calculator/Add?left=1123&right=1221");

    private HttpRequestMessage FSharp1123Plus1221Message =>
        new(HttpMethod.Get, "https://localhost:5001/add?v1123=1&v2=1221");

    private static HttpRequestMessage GetMessage(string url)
    {
        return new HttpRequestMessage(HttpMethod.Get, url);
    }

    [GlobalSetup]
    public void GlobalSetUp()
    {
        _cSharpFactory = new WebApplicationFactory<Startup>();
        _cSharpClient = _cSharpFactory.CreateClient();
        _fSharpFactory = new FSharpWebApplicationFactory();
        _fSharpClient = _fSharpFactory.CreateClient();
    }

    [Benchmark(Description = "C# 1123 + 1221")]
    public void CSharpPlus()
    {
        var r1 = _cSharpClient.SendAsync(CSharp1123Plus1221Message).GetAwaiter().GetResult();
    }


    [Benchmark(Description = "F# 1123 + 1221")]
    public void FSharp()
    {
        var r1 = _fSharpClient.SendAsync(FSharp1123Plus1221Message).GetAwaiter().GetResult();
    }

    public HttpRequestMessage CSharp13Minus12Message =>
        new(HttpMethod.Get, "https://localhost:5001/Calculator/Sub?left=13&right=12");


    [Benchmark(Description = "C# 13 - 12")]
    public void CSharpMinus()
    {
        var r1 = _cSharpClient.SendAsync(CSharp13Minus12Message).GetAwaiter().GetResult();
    }

    public HttpRequestMessage FSharp13Minus12Message => new(HttpMethod.Get, "https://localhost:5001/sub?v1=13&v2=12");

    [Benchmark(Description = "F# 13 - 12")]
    public void FSharpMinus()
    {
        var r1 = _fSharpClient.SendAsync(FSharp13Minus12Message).GetAwaiter().GetResult();
    }


    public HttpRequestMessage CSharp34Mul2Message =>
        new(HttpMethod.Get, "https://localhost:5001/Calculator/Mul?left=34&right=2");

    [Benchmark(Description = "C# 34 * 2")]
    public void CSharpMul()
    {
        var r1 = _cSharpClient.SendAsync(CSharp34Mul2Message).GetAwaiter().GetResult();
    }

    public HttpRequestMessage FSharp34Mul2Message => new(HttpMethod.Get, "https://localhost:5001/mul?v1=34&v2=2");

    [Benchmark(Description = "F# 34 * 2")]
    public void FSharpMul()
    {
        var r1 = _fSharpClient.SendAsync(FSharp34Mul2Message).GetAwaiter().GetResult();
    }

    public HttpRequestMessage CSharp56Div2Message =>
        new(HttpMethod.Get, "https://localhost:5001/Calculator/Div?left=56&right=2");

    [Benchmark(Description = "C# 56 / 2")]
    public void CSharpDiv()
    {
        var r1 = _cSharpClient.SendAsync(CSharp56Div2Message).GetAwaiter().GetResult();
    }

    public HttpRequestMessage FSharp56Div2Message => new(HttpMethod.Get, "https://localhost:5001/div?v1=56&v2=2");

    [Benchmark(Description = "F# 56 / 2")]
    public void FSharpDiv()
    {
        var r1 = _fSharpClient.SendAsync(FSharp56Div2Message).GetAwaiter().GetResult();
    }

    [GlobalCleanup]
    public void Dispose()
    {
        _cSharpClient?.Dispose();
        _cSharpFactory?.Dispose();
        _fSharpFactory?.Dispose();
        _fSharpClient?.Dispose();
    }
}