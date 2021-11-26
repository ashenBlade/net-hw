using System;
using HW9;
using Microsoft.Extensions.DependencyInjection;

namespace HW10.Infrastructure
{
    public static class CalculatorExtensions
    {
        public static IServiceCollection AddCalculator(this IServiceCollection collection)
        {
            collection = collection ?? throw new ArgumentNullException(nameof(collection));
            collection.AddTransient<ITokenizer, SimpleTokenizer>();
            collection.AddTransient<IMathExpressionTreeBuilder, ConstantMathExpressionTreeBuilder>();
            collection.AddTransient<ICalculator>(provider => new CachingCalculator(new SimpleCalculator(),
                                                                                   provider
                                                                                      .GetRequiredService<
                                                                                           CalculatorDbContext>()));
            collection.AddTransient<IMathExpressionSolver, SimpleMathExpressionSolver>();
            return collection;
        }
    }
}