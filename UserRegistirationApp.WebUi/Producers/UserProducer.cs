using Microsoft.Extensions.Logging;
using RabbitMq.Common;
using RabbitMQ.Client;
using UserRegistirationApp.WebUi.IntegrationEvents;

namespace UserRegistirationApp.WebUi.Producers
{
    public class UserProducer : ProducerBase<UserIntegrationEvent>
    {
        public UserProducer(
            ConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger,
            ILogger<ProducerBase<UserIntegrationEvent>> producerBaseLogger) :
            base(connectionFactory, logger, producerBaseLogger)
        {
        }

        protected override string ExchangeName => "CUSTOM_HOST.LoggerExchange";
        protected override string RoutingKeyName => "log.message";
        protected override string AppId => "LogProducer";
    }
}
