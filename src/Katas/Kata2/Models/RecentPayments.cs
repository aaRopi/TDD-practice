using System.Collections.Generic;

namespace Katas.Kata2.Models
{
    public class RecentPayments
    {
        public RecentPayments(IReadOnlyList<Payment> lastMonthPayments, IReadOnlyList<Payment> currentMonthPayments)
        {
            LastMonthPayments = lastMonthPayments;
            CurrentMonthPayments = currentMonthPayments;
        }

        public IReadOnlyList<Payment> LastMonthPayments { get; }
        public IReadOnlyList<Payment> CurrentMonthPayments { get; }
    }
}