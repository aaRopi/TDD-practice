using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Katas.Kata2.Implementations;
using Katas.Kata2.Models;
using Xunit;

namespace Katas.Tests.Unit.Kata2
{
    public class EmailSubjectComposerTests
    {
        private readonly EmailSubjectComposer _sut;

        public EmailSubjectComposerTests()
        {
            _sut = new();
        }
        
        [Fact]
        public void GetEmailSubject_ShouldThrowInvalidOperationException_GivenEmptyListOfUnusualSpendings()
        {
            Action act = () => _sut.GetEmailSubject(new List<UnusualSpending>());

            act.Should().Throw<InvalidOperationException>().WithMessage("List of unusual spendings cannot be empty");
        }
        
        [Fact]
        public void GetEmailSubject_ShouldReturnValidSubject_GivenNonEmptyListOfUnusualSpendings()
        {
            IReadOnlyList<UnusualSpending> unusualSpendings = new List<UnusualSpending>
            {
                new(Category.Groceries, 345.56f, 176.4f),
                new(Category.Golf, 1345.56f, 447.34f)
            };

            string expected = $"Unusual spending of €{unusualSpendings.Sum(us => us.TotalSpendingCurrentMonth)} detected!";

            string response = _sut.GetEmailSubject(unusualSpendings);

            response.Should().BeEquivalentTo(expected);
        }
    }
}