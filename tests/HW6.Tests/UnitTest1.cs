using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApplication;
using Xunit;
using Xunit.Abstractions;

namespace HW6.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<StartUp.StartUp>>
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var x = new WebApplicationFactory<StartUp.StartUp>();
            var client = x.CreateClient();
            var msg = new HttpRequestMessage { RequestUri = new Uri("http://localhost:5000/add?v1=12&v2=34") };

            var answ = client.Send(msg);
            _testOutputHelper.WriteLine($"{answ.StatusCode}");
        }
    }
}