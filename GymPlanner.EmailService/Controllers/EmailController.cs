using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpGet]
        public IActionResult Email()
        {
            var message = new MimeMessage();
            //от кого отправляем и заголовок
            message.From.Add(new MailboxAddress("Test Project",HiddenData.SenderEmailLogin));
            //кому отправляем
            message.To.Add(new MailboxAddress("Tom", "test@test.ru"));
            //тема письма
            message.Subject = "Тестовое письмо для приятеля!";
            //тело письма
            message.Body = new TextPart("plain")
            {
                Text = "Доброго времени суток. Если вы получили это пис"
            };
            using (var client = new SmtpClient())
            {
                //Указываем smtp сервер почты и порт
                client.Connect("smtp.gmail.com", 587, false);
                //Указываем свой Email адрес и пароль приложения
                client.Authenticate(HiddenData.SenderEmailLogin, HiddenData.SenderEmailPassword);
                client.Send(message);
                client.Disconnect(true);
            }
            return Ok();
        }
    }
}
