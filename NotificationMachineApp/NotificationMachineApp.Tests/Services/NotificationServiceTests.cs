using Microsoft.Extensions.Logging;
using Moq;
using NotificationMachineApp.Core.Interfaces.Services;
using NotificationMachineApp.Core.Models;
using NotificationMachineApp.Core.Models.Interfaces;

namespace NotificationMachineApp.Tests.Services
{
    public class NotificationServiceTests
    {
        private readonly NotificationService _notificationService;
        private readonly Mock<IKafkaProducerService> _kafkaProducerServiceMock;
        private readonly Mock<ILogger<NotificationService>> _loggerMock;

        public NotificationServiceTests()
        {
            _kafkaProducerServiceMock = new Mock<IKafkaProducerService>();
            _loggerMock = new Mock<ILogger<NotificationService>>();
            _notificationService = new NotificationService(_kafkaProducerServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task QueueNotificationTasksAsync_ShouldQueueNotificationsForEachCustomer()
        {
            // Arrange
            var customer1Mock = new Mock<ICustomer>();
            customer1Mock.Setup(c => c.GetUserId()).Returns("1");
            customer1Mock.Setup(c => c.GetEmail()).Returns("customer1@example.com");
            customer1Mock.Setup(c => c.GetFullName()).Returns("Customer One");

            var customer2Mock = new Mock<ICustomer>();
            customer2Mock.Setup(c => c.GetUserId()).Returns("2");
            customer2Mock.Setup(c => c.GetEmail()).Returns("customer2@example.com");
            customer2Mock.Setup(c => c.GetFullName()).Returns("Customer Two");

            var customers = new List<ICustomer>
            {
                customer1Mock.Object,
                customer2Mock.Object
            };

            var topic = "customer-notifications";
            var message = "Hello, you've got a notification!";

            // Act
            await _notificationService.QueueNotificationTasksAsync(customers, message, topic);

            // Assert
            foreach (var customer in customers)
            {
                _kafkaProducerServiceMock.Verify(k => k.ProduceAsync(
                    topic,
                    It.Is<NotificationMessage>(m =>
                        m.CustomerId == customer.GetUserId() &&
                        m.CustomerEmail == customer.GetEmail() &&
                        m.Message == message
                    )
                ), Times.Once);
            }
        }
    }
}
