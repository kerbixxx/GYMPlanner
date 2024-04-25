using EmailService.DTOs;
using Hangfire.Common;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EmailService.Services
{
    public class RabbitMqListener
    {
        private IConnection _connection;
        private IModel _channel;

        public void Consume()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost", Port = 5673 };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: "EmailQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                MessagePlanEditConsumer message = new();
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);
                    message = JsonConvert.DeserializeObject<MessagePlanEditConsumer>(json);
                    SendEmail(message.SubscriberEmail, message.PlanName);
                };

                _channel.BasicConsume(queue: "EmailQueue", autoAck: true, consumer: consumer);
            }
            finally
            {
                // Выключение
                _channel?.Close();
                _connection?.Close();
            }
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
        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
