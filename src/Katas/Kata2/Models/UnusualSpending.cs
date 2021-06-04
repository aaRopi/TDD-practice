namespace Katas.Kata2.Models
{
    public class UnusualSpending
    {
        public UnusualSpending(Category category, float totalSpending)
        {
            Category = category;
            TotalSpending = totalSpending;
        }

        public float TotalSpending { get; }
        public Category Category { get; }
    }
}