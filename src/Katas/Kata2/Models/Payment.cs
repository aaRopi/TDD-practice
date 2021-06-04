namespace Katas.Kata2.Models
{
    public class Payment
    {
        public Payment(float price, string description, Category category)
        {
            Price = price;
            Description = description;
            Category = category;
        }

        public float Price { get; }
        public string Description { get; }
        public Category Category { get; }
    }
}