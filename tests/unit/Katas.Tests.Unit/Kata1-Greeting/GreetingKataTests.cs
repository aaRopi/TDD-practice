using FluentAssertions;
using Xunit;

namespace Katas.Tests.Unit
{
    public class GreetingKataTests
    {
        [Theory]
        [InlineData("Hello, Bob.", "Bob")]
        [InlineData("Hello, my friend.", null)]
        [InlineData("HELLO JERRY!", "JERRY")]
        [InlineData("Hello, Jill and Jane.", "Jill", "Jane")]
        [InlineData("Hello, Amy, Brian, and Charlotte.", "Amy", "Brian", "Charlotte")]
        [InlineData("Hello, Amy and Charlotte. AND HELLO BRIAN!", "Amy", "BRIAN", "Charlotte")]
        [InlineData("Hello, Bob, Charlie, and Dianne.", "Bob", "Charlie, Dianne")]
        public void Greet_ShouldReturnGreetingWithAllNames_WhenCalledWithMultipleNames(string expectedResponse, params string[] nameArgs)
        {
            GreetingKata sut = new();

            string response = sut.Greet(nameArgs);

            response.Should().Be(expectedResponse);
        }
    }
}