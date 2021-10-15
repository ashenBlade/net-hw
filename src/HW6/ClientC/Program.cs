using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientC
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var msg = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/add?v1=1&v2=2");
            var answ = await client.SendAsync(msg);
            var body = await answ.Content.ReadAsStringAsync();
            Console.WriteLine($"{body}");
        }
    }
}