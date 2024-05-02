using GymPlanner.Application.Interfaces.Repositories.Plan;
using GymPlanner.Application.Interfaces.Services;
using GymPlanner.Application.Models.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IRabbitMQProducer _producer;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IRabbitMQProducer producer)
        {
            _subscriptionRepository = subscriptionRepository;
            _producer = producer;
        }

        public bool IsUserSubbedToPlan(int userId, int planId)
        {
            return _subscriptionRepository.FirstOrDefault(p => (p.PlanId == planId) && (p.UserId == userId))!=null;
        }

        public async Task NotifyAllSubscribers(int planId)
        {
            var allSubscribers = await _subscriptionRepository.GetSubscriptionsOnPlanAsync(planId);
            foreach (var subscriber in allSubscribers)
            {
                _producer.NotifySubscribersAboutEdit(new MessageEditNotifier() { PlanName = subscriber.Plan.Name, SubscriberEmail = subscriber.User.Email });

            }
            _producer.Dispose();
        }
    }
}
