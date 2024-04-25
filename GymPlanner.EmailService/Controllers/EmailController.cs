using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using EmailService.Services;
using Hangfire;

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly RabbitMqListener _rabbitMqListener;
        public EmailController()
        {
            _rabbitMqListener = new RabbitMqListener();
        }
        [HttpGet]
        public IActionResult Email()
        {
            SendEmail("test@test.com", "test");
            return Ok();
        }

        private void SendEmail(string email, string planName)
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
