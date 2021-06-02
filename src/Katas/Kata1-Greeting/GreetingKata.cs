using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Katas
{
    public class GreetingKata
    {
        public string Greet(params string[]? names)
        {
            if (names == null)
            {
                return "Hello, my friend.";
            }

            names = names
                .SelectMany(name =>
                {
                    if (name.Contains('\"'))
                    {
                        return new[] { Regex.Unescape(name).Trim('\"') };
                    }
                    
                    return name.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                })
                .ToArray();
            
            string[] shoutedNames = names.Where(name => name.All(char.IsUpper)).ToArray();
            string[] regularNames = names.Except(shoutedNames).ToArray();

            string regularGreeting = GetRegularGreeting(regularNames);
            string shoutedGreeting = GetShoutedGreeting(shoutedNames);

            if (string.IsNullOrWhiteSpace(regularGreeting))
            {
                return shoutedGreeting;
            }

            return $"{regularGreeting}{(string.IsNullOrWhiteSpace(shoutedGreeting) ? string.Empty : $" AND {shoutedGreeting}")}";
        }

        private string GetShoutedGreeting(string[] names)
        {
            if (names.Length == 1)
            {
                return $"HELLO {names[0]}!";
            }

            return string.Empty;
        }

        private string GetRegularGreeting(string[] names)
        {
            return names.Length switch
            {
                0 => string.Empty,
                1 => $"Hello, {names[0]}.",
                2 => $"Hello, {names[0]} and {names[1]}.",
                _ => $"Hello, {string.Join(", ", names.Take(names.Length - 1))}, and {names.Last()}."
            };
        }
    }
}