using System;
using System.Collections.Generic;

namespace HW7.NameGenerator
{
    public class FancyNameFormatter : INameFormatter
    {
        private static readonly List<string> Wrappers = new() { "%", "!!!", "**", "OMG" };
        private readonly Random _random = new();

        private bool Success =>
            _random.Next(0, 10) is 4 or 5;

        private string GetRandomWrapper()
        {
            return Wrappers[_random.Next(0, Wrappers.Count - 1)];
        }

        public string FormatName(string name)
        {
            return Success
                       ? $"{GetRandomWrapper()} {name} {GetRandomWrapper()}"
                       : name;
        }
    }
}