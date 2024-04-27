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
                _channel?.Close();
                _connection?.Close();
            }
        }
        
        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
