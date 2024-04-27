namespace EmailService.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string email, string planName);
    }
}
