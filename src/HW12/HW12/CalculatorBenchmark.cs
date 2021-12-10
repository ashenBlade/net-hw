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

    private HttpRequestMessage CSharp1Plus1Message =>
        new(HttpMethod.Get, "https://localhost:5001/Calculator/Add?left=1&right=1");

    private HttpRequestMessage FSharp1Plus1Message => new(HttpMethod.Get, "https://localhost:5001/add?v1=1&v2=1");

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

    [Benchmark(Description = "C# Server with 1 + 1")]
    public void CSharp()
    {
        var r1 = _cSharpClient.SendAsync(CSharp1Plus1Message).GetAwaiter().GetResult();
    }

    [Benchmark(Description = "F# Server with 1 + 1")]
    public void FSharp()
    {
        var r1 = _fSharpClient.SendAsync(FSharp1Plus1Message).GetAwaiter().GetResult();
    }

    [GlobalCleanup]
    public void Dispose()
    {
        _cSharpClient?.Dispose();
        _cSharpFactory?.Dispose();
    }
}