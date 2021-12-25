using System;
using HW10;
using HW10.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HW13.Tests
{
    public class CalculatorCacheFixture : IDisposable
    {
        public WebApplicationFactory<Startup> Factory { get; }

        public CalculatorCacheFixture()
        {
            Factory = new WebApplicationFactory<Startup>()
               .WithWebHostBuilder(builder =>
                                       builder
                                          .ConfigureServices(services =>
                                                                 services
                                                                    .AddDbContext
                                                                         <CalculatorDbContext>(options =>
                                                                                                   options
                                                                                                      .UseInMemoryDatabase("database"))));
        }

        public void Dispose()
        {
            Factory.Dispose();
        }
    }
}