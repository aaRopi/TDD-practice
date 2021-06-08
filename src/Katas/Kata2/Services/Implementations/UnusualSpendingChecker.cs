using System;
using System.Collections.Generic;
using System.Linq;
using Katas.Kata2.Models;

namespace Katas.Kata2.Implementations
{
    public class UnusualSpendingChecker : IUnusualSpendingChecker
    {
        public IReadOnlyList<UnusualSpending> CheckUnusualMonthlySpending(RecentPayments recentPayments)
        {
            IReadOnlyList<GroupedSpend> groupedLastMonthSpends = recentPayments.LastMonthPayments
                .GroupBy(p => p.Category)
                .Select(grouping => new GroupedSpend(grouping.Key, grouping.Sum(p => p.Price)))
                .ToList().AsReadOnly();
            
            IReadOnlyList<GroupedSpend> groupedCurrentMonthSpends = recentPayments.CurrentMonthPayments
                .GroupBy(p => p.Category)
                .Select(grouping => new GroupedSpend(grouping.Key, grouping.Sum(p => p.Price)))
                .ToList().AsReadOnly();

            List<UnusualSpending> unusualSpendings = new();

            foreach (GroupedSpend currentMonthSpend in groupedCurrentMonthSpends)
            {
                GroupedSpend? lastMonthSpend =
                    groupedLastMonthSpends.FirstOrDefault(gs => gs.Category == currentMonthSpend.Category);

                if (lastMonthSpend == null)
                {
                    unusualSpendings.Add(new UnusualSpending(currentMonthSpend.Category, currentMonthSpend.TotalSpend, 0.0f));
                }
                else if (currentMonthSpend.TotalSpend > lastMonthSpend.TotalSpend && Math.Abs(currentMonthSpend.TotalSpend - 1.5f * lastMonthSpend.TotalSpend) > 0.1f)
                {
                    unusualSpendings.Add(new UnusualSpending(currentMonthSpend.Category, currentMonthSpend.TotalSpend, lastMonthSpend.TotalSpend));
                }
            }

            return unusualSpendings.AsReadOnly();
        }
    }
}