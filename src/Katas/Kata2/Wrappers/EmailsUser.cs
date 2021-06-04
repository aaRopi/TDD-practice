namespace Katas.Kata2
{
    public class EmailsUser
    {
        public static EmailsUser Instance => new EmailsUser();
        
        private EmailsUser() {}

        public void Email(int userId, string subject, string body)
        {
            throw new System.NotImplementedException("Implementing this is Somebody Else's Job.");
        }
    }
}