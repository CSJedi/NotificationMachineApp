using Microsoft.Extensions.Logging;
using NotificationMachineApp.Core.Interfaces.Services;
using NotificationMachineApp.Core.Models;
using NotificationMachineApp.Core.Models.Interfaces;

public class NotificationService : INotificationService
{
    private readonly IKafkaProducerService _kafkaProducerService;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(IKafkaProducerService kafkaProducerService, ILogger<NotificationService> logger)
    {
        _kafkaProducerService = kafkaProducerService;
        _logger = logger;
    }

    public async Task QueueNotificationTasksAsync(IEnumerable<ICustomer> customers, string message, string topic)
    {
        _logger.LogInformation($"Queuing notification tasks for customers on topic {topic}.");
        foreach (var customer in customers)
        {
            await _kafkaProducerService.ProduceAsync(topic, new NotificationMessage 
            {
                CustomerId = customer.GetUserId(), 
                CustomerEmail = customer.GetEmail(), 
                CustomerName = customer.GetFullName(), 
                Message = message 
            });
            _logger.LogInformation($"Queued notification for {customer.GetFullName()} ({customer.GetEmail()}).");
        }
    }
}
