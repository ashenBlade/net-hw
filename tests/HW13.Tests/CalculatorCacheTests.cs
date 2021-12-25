using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

[assembly: SuppressXUnitOutputException]
namespace HW13.Tests
{
    public class CalculatorCacheTests : IClassFixture<CalculatorCacheFixture>
    {
        private readonly CalculatorCacheFixture _fixture;
        private readonly ITestOutputHelper _testOutputHelper;
        private HttpClient NewClient => _fixture.Factory.CreateClient();

        public CalculatorCacheTests(CalculatorCacheFixture fixture, ITestOutputHelper testOutputHelper)
        {
            _fixture = fixture;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        [DotMemoryUnit(FailIfRunWithoutSupport = true, CollectAllocations = true)]
        public void TestsForDotMemoryUnitAllocations()
        {
            DotMemoryUnitTestOutput.SetOutputMethod(_testOutputHelper.WriteLine);
            const int TotalRequestsCount = 1_000;
            const int PacketSize = 1_000;
            const int IterationsCount = TotalRequestsCount / PacketSize;
            using var client = NewClient;
            var previousSnapshot = dotMemory.Check();
            var generator = new ExpressionGenerator("@ + @ - @ * @ - @");
            var totalAllocated = 0L;
            for (var i = 0; i < IterationsCount; i++)
            {
                var expression = generator
                   .GenerateExpression();
                var packets = Enumerable.Range(0, PacketSize)
                                        .Select(_ => ( Task ) client.SendAsync(GetHttpRequestMessage(expression)))
                                        .ToArray();
                Task.WaitAll(packets);
                var currentExpressionSize = Encoding.UTF8.GetBytes(expression).Length;
                previousSnapshot = dotMemory.Check(memory =>
                {
                    var difference = memory.GetDifference(previousSnapshot);
                    var newObjects = difference.GetNewObjects();
                    totalAllocated += newObjects.SizeInBytes;
                    _testOutputHelper.WriteLine($"{i}: Allocated = {newObjects.SizeInBytes} bytes");
                    Assert.True(0 <= newObjects.SizeInBytes);
                    Assert.True(currentExpressionSize <= newObjects.SizeInBytes);
                });
            }
            _testOutputHelper.WriteLine("");
            _testOutputHelper.WriteLine($"Allocated total: {totalAllocated} bytes");
        }

        private static HttpRequestMessage GetHttpRequestMessage(string expression)
        {
            return new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/Calculator/Calculate")
                   {
                       Content = new FormUrlEncodedContent(new[]
                                                           {
                                                               new KeyValuePair<string?, string?>("expression",
                                                                                                  expression)
                                                           })
                   };
        }

        private static async Task SendAsync(HttpClient client, string expression)
        {
            var message = GetHttpRequestMessage(expression);
            var response = await client.SendAsync(message);
        }

        [Theory]
        [InlineData("@ + @ * @")]
        [InlineData("(10 * @) / 13 + 123")]
        [InlineData("((@ / 10) / 8 - @)")]
        [InlineData("@ + @ - @ - @ - @")]
        [InlineData("@ + 23 * @ + @ - @ / 12")]
        [InlineData("@ / 29 - 23 * @ * @ - @ / 12")]
        [InlineData("@ + @ + @ - 12")]
        [InlineData("@ + @ * (@ / @)")]
        [InlineData("@ - @ * @ * 2")]
        public async Task Calculate(string pattern, int repeat = 100000)
        {
            var generator = new ExpressionGenerator(pattern);
            using var client = NewClient;
            for (var i = 0; i < repeat; i++)
            {
                await SendAsync(client, generator.GenerateExpression());
            }
        }
    }
}