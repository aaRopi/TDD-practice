namespace Katas.Kata2.Models
{
    public class UnusualSpending
    {
        public UnusualSpending(Category category, float totalSpendingCurrentMonth, float totalSpendingLastMonth)
        {
            Category = category;
            TotalSpendingCurrentMonth = totalSpendingCurrentMonth;
            TotalSpendingLastMonth = totalSpendingLastMonth;
        }

        public float TotalSpendingCurrentMonth { get; }
        public float TotalSpendingLastMonth { get; }
        public Category Category { get; }
    }
}