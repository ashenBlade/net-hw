using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace HW13.Tests
{
    public class CalculatorCacheTests : IClassFixture<CalculatorCacheFixture>
    {
        private readonly CalculatorCacheFixture _fixture;
        private HttpClient NewClient => _fixture.Factory.CreateClient();

        public CalculatorCacheTests(CalculatorCacheFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [DotMemoryUnit(FailIfRunWithoutSupport = true, CollectAllocations = true)]
        public void TestsForDotMemoryUnitAllocations()
        {
            const int TotalRequestsCount = 1_000;
            const int IterationsCount = 1_000;
            const int PacketSize = TotalRequestsCount / IterationsCount;
            using var client = NewClient;
            var previousSnapshot = dotMemory.Check();
            var generator = new ExpressionGenerator("@ + @ - @ * @ - @");
            for (var i = 0; i < IterationsCount; i++)
            {
                var packets = Enumerable.Range(0, PacketSize)
                                        .Select(_ =>
                                                    ( Task ) client.SendAsync(GetHttpRequestMessage(generator
                                                                                                       .GenerateExpression())))
                                        .ToArray();
                Task.WaitAll(packets);

                previousSnapshot = dotMemory.Check(memory =>
                {
                    Assert.NotEqual(0, memory.GetDifference(previousSnapshot)
                                             .GetNewObjects(where => where.Type.Is<string>())
                                             .ObjectsCount);
                });
            }
            // dotMemory.Check(memory =>
            // {
            //
            // });
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