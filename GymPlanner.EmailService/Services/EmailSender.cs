using EmailService.Interfaces;
using System.Net.Mail;

namespace EmailService.Services
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string email, string planName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("GymPlanner", HiddenData.SenderEmailLogin));
            message.To.Add(new MailboxAddress("Пользователь", email));
            message.Subject = $"Изменен план";
            message.Body = new TextPart("plain")
            {
                Text = $"Доброго времени суток. Если вы получили это письмо, значит вы были подписаны на план {planName} и он был изменен."
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(HiddenData.SenderEmailLogin, HiddenData.SenderEmailPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
