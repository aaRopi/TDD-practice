using System.Collections.Generic;
using Katas.Kata2;
using Katas.Kata2.Models;
using NSubstitute;
using Xunit;

namespace Katas.Tests.Unit.Kata2
{
    public class UnusualSpendingKataTests
    {
        private readonly ISpending _spending = Substitute.For<ISpending>();
        private readonly IUnusualSpendingChecker _spendingChecker = Substitute.For<IUnusualSpendingChecker>();
        private readonly IUserWarningService _userWarningService = Substitute.For<IUserWarningService>();
        
        private readonly UnusualSpendingKata _sut;

        public UnusualSpendingKataTests()
        {
            _sut = new UnusualSpendingKata(_spending, _spendingChecker, _userWarningService);
        }

        [Fact]
        public void Trigger_ShouldTriggerEmailWarningService_WhenFindsUnusualSpending()
        {
            // Arrange
            int userId = 35;

            RecentPayments recentPayments = new(new Payment[]{}, new Payment[]{});
            IReadOnlyList<UnusualSpending> unusualSpendings = new UnusualSpending[] { new(Category.Entertainment, 137.89f) };

            _spending.FetchRecentPayments(userId).Returns(recentPayments);

            _spendingChecker.CheckUnusualMonthlySpending(recentPayments)
                .Returns(unusualSpendings);

            // Act
            _sut.TriggersUnusualSpendingEmail(userId);

            // Assert
            _spending.Received().FetchRecentPayments(userId);

            _spendingChecker.Received().CheckUnusualMonthlySpending(recentPayments);

            _userWarningService.Received().WarnUserWithEmail(userId, unusualSpendings);
        }
        
        [Fact]
        public void Trigger_ShouldNotTriggerEmailWarningService_WhenFindsNoUnusualSpending()
        {
            // Arrange
            int userId = 35;

            RecentPayments recentPayments = new(new Payment[]{}, new Payment[]{});
            IReadOnlyList<UnusualSpending> unusualSpendings = new UnusualSpending[] { };

            _spending.FetchRecentPayments(userId).Returns(recentPayments);

            _spendingChecker.CheckUnusualMonthlySpending(recentPayments)
                .Returns(unusualSpendings);

            // Act
            _sut.TriggersUnusualSpendingEmail(userId);

            // Assert
            _spending.Received().FetchRecentPayments(userId);

            _spendingChecker.Received().CheckUnusualMonthlySpending(recentPayments);

            _userWarningService.Received(0).WarnUserWithEmail(userId, unusualSpendings);
        }
    }
}