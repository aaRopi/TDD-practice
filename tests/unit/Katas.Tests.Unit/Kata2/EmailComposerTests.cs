using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Katas.Kata2.Implementations;
using Katas.Kata2.Models;
using Xunit;

namespace Katas.Tests.Unit.Kata2
{
    public class EmailComposerTests
    {
        private readonly EmailComposer _sut;

        public EmailComposerTests()
        {
            _sut = new EmailComposer();
        }

        [Fact]
        public void Compose_ShouldThrowInvalidOperationException_GivenEmptyListOfUnusualSpendings()
        {
            Action act = () => _sut.Compose(new List<UnusualSpending>());

            act.Should().Throw<InvalidOperationException>().WithMessage("List of unusual spendings cannot be empty");
        }
        
        [Fact]
        public void Compose_ShouldReturnEmailWithValidSubjectAndBody_GivenNonEmptyListOfUnusualSpendings()
        {
            IReadOnlyList<UnusualSpending> unusualSpendings = new List<UnusualSpending>
            {
                new(Category.Groceries, 345.56f),
                new(Category.Golf, 1345.56f)
            };

            string subject = $"Unusual spending of €{unusualSpendings.Sum(us => us.TotalSpending)} detected!";
            string body = "Hello card user!\n\n" +
                          "We have detected unusually high spending on your card in these categories:" +
                          $"\n* You spent €{unusualSpendings[0].TotalSpending} on {unusualSpendings[0].Category.ToString().ToLower()}" +
                          $"\n* You spent €{unusualSpendings[1].TotalSpending} on {unusualSpendings[1].Category.ToString().ToLower()}" +
                          "\n\nLove," +
                          "\n\nThe Credit Card Company";

            Email expectedEmail = new (subject, body);
            
            Email email = _sut.Compose(unusualSpendings);

            email.Should().BeEquivalentTo(expectedEmail);
        }
    }
}