using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Services
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;

        public RabbitMQProducer()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5673
            };
            try
            {
                _connection = _factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения к брокеру сообщений: {ex.Message}");
            }
        }

        public void NotifySubscribersAboutEdit(MessageEditNotifier message)
        {
            if (_connection == null)
            {
                return;
            }
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: "EmailQueue",
                                durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "",
                               routingKey: "EmailQueue",
                               basicProperties: null,
                               body: body);
            }
        }
        public void SendMessageToRabbit<T>(T message)
        {
            if (_connection == null)
            {
                return;
            }
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: "MyQueue",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "",
                               routingKey: "MyQueue",
                               basicProperties: null,
                               body: body);
            }
        }
        public void Dispose()
        {
            if (_connection == null)
            {
                return;
            }
            _connection?.Dispose();
        }

    }
}
