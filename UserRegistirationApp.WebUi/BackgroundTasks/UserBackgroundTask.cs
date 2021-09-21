using Microsoft.Extensions.Hosting;
using RabbitMq.Common;
using RabbitMq.Common.IntegrationEvent;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserRegistirationApp.WebUi.IntegrationEvents;

namespace UserRegistirationApp.WebUi.BackgroundTasks
{
    public class UserBackgroundTask  
    {
        private readonly IRabbitMqProducer<UserIntegrationEvent> _producer;

        public UserBackgroundTask(IRabbitMqProducer<UserIntegrationEvent> producer) => _producer = producer;

        protected   async Task ExecuteAsync(CancellationToken stoppingToken,string message)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var @event = new UserIntegrationEvent
                {
                    Id = Guid.NewGuid(),
                    Message = $"{message}"
                };

                _producer.Publish(@event);
             }

            await Task.CompletedTask;
        }
    }
}
