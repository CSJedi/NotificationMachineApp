using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotificationMachineApp.Core.Interfaces.Services;
using NotificationMachineApp.Core.Models;
using System.Text.Json;

namespace NotificationMachineApp.Infrastructure.Kafka
{
    public class KafkaConsumerService : IKafkaConsumerService
    {
        private IConsumer<string, string> _consumer;
        private readonly ConsumerConfig _consumerConfig;
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public KafkaConsumerService(string bootstrapServers, string groupId, IServiceScopeFactory scopeFactory, ILogger<KafkaConsumerService> logger)
        {
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task ConsumeAsync(string topic)
        {
            _consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            _consumer.Subscribe(topic);
                   
            try
            {
                var consumeResult = _consumer.Consume(TimeSpan.FromMilliseconds(1000));
                if (consumeResult != null)
                {
                    _logger.LogInformation($"Received message on {consumeResult.Topic}: {consumeResult.Message.Value}");
                    var notificationMessage = JsonSerializer.Deserialize<NotificationMessage>(consumeResult.Message.Value);
                    if(notificationMessage == null)
                    {
                        _logger.LogError("Failed to deserialize message");
                        return;
                    }
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                        await emailService.SendEmailAsync(notificationMessage.CustomerEmail, $"Notification message for {notificationMessage.CustomerName}", notificationMessage.Message);
                    }
                }
            }
            catch (ConsumeException e)
            {
                _logger.LogError($"Error occurred: {e.Error.Reason}");
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Consumer closed");
            }
            finally
            {
                _consumer.Close();
            }
        }
    }
}
