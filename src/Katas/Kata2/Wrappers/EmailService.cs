namespace Katas.Kata2
{
    public class EmailService : IEmailService
    {
        private readonly EmailsUser _emailsUser = EmailsUser.Instance;
        
        public void Email(int userId, string subject, string body)
        {
            _emailsUser.Email(userId, subject, body);
        }
    }
}