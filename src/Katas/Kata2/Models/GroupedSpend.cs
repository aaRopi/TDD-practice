namespace Katas.Kata2.Models
{
    public class GroupedSpend
    {
        public GroupedSpend(Category category, float totalSpend)
        {
            Category = category;
            TotalSpend = totalSpend;
        }

        public Category Category { get; }
        public float TotalSpend { get; }
    }
}