namespace Katas.Kata2.Models
{
    public class Email
    {
        public Email(string subject, string body)
        {
            Subject = subject;
            Body = body;
        }

        public string Subject { get; }
        public string Body { get; }
    }
}