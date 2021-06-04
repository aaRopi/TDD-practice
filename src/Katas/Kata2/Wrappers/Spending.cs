using System;
using System.Collections.Generic;
using Katas.Kata2.Models;

namespace Katas.Kata2
{
    public class Spending : ISpending
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserSpendingFetcher _userSpendingFetcher;

        public Spending(IDateTimeProvider dateTimeProvider, IUserSpendingFetcher userSpendingFetcher)
        {
            _dateTimeProvider = dateTimeProvider;
            _userSpendingFetcher = userSpendingFetcher;
        }

        public RecentPayments FetchRecentPayments(int userId)
        {
            DateTime today = _dateTimeProvider.DateTimeNow;
            DateTime oneMonthAgo = today.AddMonths(-1);

            IReadOnlyList<Payment> currentMonthPayments = _userSpendingFetcher.Fetch(userId, today.Year, today.Month);
            IReadOnlyList<Payment> lastMonthPayments = _userSpendingFetcher.Fetch(userId, oneMonthAgo.Year, oneMonthAgo.Month);

            return new RecentPayments(lastMonthPayments, currentMonthPayments);
        }
    }
}