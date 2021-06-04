using System;
using System.Collections.Generic;
using FluentAssertions;
using Katas.Kata2;
using Katas.Kata2.Models;
using NSubstitute;
using Xunit;

namespace Katas.Tests.Unit.Kata2
{
    public class SpendingTests
    {
        private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        private readonly IUserSpendingFetcher _userSpendingFetcher = Substitute.For<IUserSpendingFetcher>();
        
        private readonly Spending _sut;

        public SpendingTests()
        {
            _sut = new Spending(_dateTimeProvider, _userSpendingFetcher);
        }

        [Fact]
        public void FetchRecentPayments_ShouldFetchRecentPayments_GivenUserId()
        {
            int userId = 56;
            DateTime testDay = new DateTime(2023, 7, 24);
            DateTime oneMonthAgo = testDay.AddMonths(-1);
            _dateTimeProvider.DateTimeNow.Returns(testDay);
            
            IReadOnlyList<Payment> currentMonthPayments = new List<Payment>();
            IReadOnlyList<Payment> lastMonthPayments = new List<Payment>();

            _userSpendingFetcher.Fetch(userId, testDay.Year, testDay.Month).Returns(currentMonthPayments);
            _userSpendingFetcher.Fetch(userId, oneMonthAgo.Year, oneMonthAgo.Month).Returns(lastMonthPayments);
            
            RecentPayments response = _sut.FetchRecentPayments(userId);

            _userSpendingFetcher.Received().Fetch(userId, testDay.Year, testDay.Month);
            _userSpendingFetcher.Received().Fetch(userId, oneMonthAgo.Year, oneMonthAgo.Month);
            
            response.CurrentMonthPayments.Should().BeSameAs(currentMonthPayments);
            response.LastMonthPayments.Should().BeSameAs(lastMonthPayments);
        }
    }
}