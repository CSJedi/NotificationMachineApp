using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using NotificationMachineApp.Core.Interfaces.Services;
using System.Text.Json;

namespace NotificationMachineApp.Infrastructure.Kafka
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly ProducerConfig _producerConfig;
        private readonly ILogger<KafkaProducerService> _logger;

        public KafkaProducerService(string bootstrapServers, ILogger<KafkaProducerService> logger)
        {
            _producerConfig = new ProducerConfig { BootstrapServers = bootstrapServers };
            _logger = logger;
        }

        public async Task ProduceAsync<T>(string topic, T message)
        {
            using var producer = new ProducerBuilder<string, string>(_producerConfig).Build();
            try
            {
                var jsonMessage = JsonSerializer.Serialize(message);
                var result = await producer.ProduceAsync(topic, new Message<string, string> { Value = jsonMessage });
                _logger.LogInformation($"Message sent to {result.Topic}");
            }
            catch (ProduceException<string, string> ex)
            {
                _logger.LogError($"Failed to deliver message: {ex.Message} [{ex.Error.Code}]");
                throw;
            }
        }
    }
}
