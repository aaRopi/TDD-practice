using System;
using System.Collections.Generic;
using FluentAssertions;
using Katas.Kata2.Implementations;
using Katas.Kata2.Models;
using Xunit;

namespace Katas.Tests.Unit.Kata2
{
    public class EmailBodyComposerTests
    {
        private readonly EmailBodyComposer _sut;

        public EmailBodyComposerTests()
        {
            _sut = new();
        }
        
        [Fact]
        public void GetEmailBody_ShouldThrowInvalidOperationException_GivenEmptyListOfUnusualSpendings()
        {
            Action act = () => _sut.GetEmailBody(new List<UnusualSpending>());

            act.Should().Throw<InvalidOperationException>().WithMessage("List of unusual spendings cannot be empty");
        }
        
        [Fact]
        public void GetEmailBody_ShouldReturnValidBody_GivenNonEmptyListOfUnusualSpendings()
        {
            IReadOnlyList<UnusualSpending> unusualSpendings = new List<UnusualSpending>
            {
                new(Category.Groceries, 345.56f, 23.0f),
                new(Category.Golf, 1345.56f, 0.0f)
            };

            string expected = "Hello card user!\n\n" +
                          "We have detected unusually high spending on your card in these categories:" +
                          $"\n* You spent €{unusualSpendings[0].TotalSpendingCurrentMonth} on {unusualSpendings[0].Category.ToString().ToLower()}. Last month this was €{unusualSpendings[0].TotalSpendingLastMonth}." +
                          $"\n* You spent €{unusualSpendings[1].TotalSpendingCurrentMonth} on {unusualSpendings[1].Category.ToString().ToLower()}. Last month this was €{unusualSpendings[1].TotalSpendingLastMonth}." +
                          "\n\nLove," +
                          "\n\nThe Credit Card Company";

            string response = _sut.GetEmailBody(unusualSpendings);

            response.Should().BeEquivalentTo(expected);
        }
    }
}