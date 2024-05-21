using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationMachineApp.Core.Interfaces.Services;

namespace NotificationMachineApp.Infrastructure.Kafka
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly ILogger<BackgroundWorkerService> _logger;
        private readonly IKafkaConsumerService _consumer;

        public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IKafkaConsumerService consumer)
        {
            _logger = logger;
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _consumer.ConsumeAsync("Topic2");
                    await _consumer.ConsumeAsync("Topic101");
                    await _consumer.ConsumeAsync("Topic145");

                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while consuming message: {ex}");
            }
        }
    }

}