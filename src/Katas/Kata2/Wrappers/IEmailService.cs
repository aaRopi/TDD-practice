namespace Katas.Kata2
{
    public interface IEmailService
    {
        void Email(int userId, string subject, string body);
    }
}