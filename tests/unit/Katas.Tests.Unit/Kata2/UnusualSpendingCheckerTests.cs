using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Katas.Kata2.Implementations;
using Katas.Kata2.Models;
using Xunit;

namespace Katas.Tests.Unit.Kata2
{
    public class UnusualSpendingCheckerTests
    {
        private readonly UnusualSpendingChecker _sut;

        public UnusualSpendingCheckerTests()
        {
            _sut = new UnusualSpendingChecker();
        }

        [Fact]
        public void CheckUnusualMonthlySpending_ShouldReturnEmptyList_GivenRecentPaymentsWithNoBigSpends()
        {
            IEnumerable<UnusualSpending> response =
                _sut.CheckUnusualMonthlySpending(new RecentPayments(new List<Payment>(), new List<Payment>()));

            response.Should().BeEmpty();
        }
        
        [Fact]
        public void CheckUnusualMonthlySpending_ShouldReturnNonEmptyList_GivenRecentPaymentsWithCurrentMonthSpendsAndNoLastMonthSpends()
        {
            IEnumerable<UnusualSpending> response =
                _sut.CheckUnusualMonthlySpending(new RecentPayments(
                    new List<Payment>(), 
                    new List<Payment> { new Payment(345.0f, "Some description", Category.Golf) }));

            response.Should().ContainSingle(us => us.Category == Category.Golf && Math.Abs(us.TotalSpendingCurrentMonth - 345.0f) < 0.01f);
        }
        
        [Fact]
        public void CheckUnusualMonthlySpending_ShouldReturnNonEmptyList_GivenRecentPaymentsWithCurrentMonthSpendsMoreThan50PercentGreaterThanLastMonthSpends()
        {
            IEnumerable<UnusualSpending> response =
                _sut.CheckUnusualMonthlySpending(new RecentPayments(
                    new List<Payment> { new Payment(100.0f, "Some spend", Category.Restaurants) }, 
                    new List<Payment>
                    {
                        new Payment(345.0f, "Some description", Category.Golf),
                        new Payment(205.0f, "Some other spend", Category.Restaurants)
                    }))
                    .ToList();

            response.Should().HaveCount(2);
            response.Should().ContainSingle(us => us.Category == Category.Golf && Math.Abs(us.TotalSpendingCurrentMonth - 345.0f) < 0.01f);
            response.Should().ContainSingle(us => us.Category == Category.Restaurants && Math.Abs(us.TotalSpendingCurrentMonth - 205.0f) < 0.01f);
        }
    }
}