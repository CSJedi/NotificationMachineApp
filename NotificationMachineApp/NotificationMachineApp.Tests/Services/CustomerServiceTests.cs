using Microsoft.Extensions.Logging;
using Moq;
using NotificationMachineApp.Core.Interfaces.Repositories;
using NotificationMachineApp.Core.Models;
using NotificationMachineApp.Core.Models.Interfaces;
using NotificationMachineApp.Infrastructure.Services;

namespace NotificationMachineApp.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<ILogger<CustomerService>> _loggerMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _loggerMock = new Mock<ILogger<CustomerService>>();
            _customerService = new CustomerService(_customerRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetQuietCustomersAsync_CallsRepositoryCorrectly()
        {
            // Arrange
            var startDate = new DateTime(2024, 4, 1);
            var endDate = new DateTime(2024, 4, 30);
            _customerRepositoryMock.Setup(repo => repo.GetQuietCustomersAsync<Customer145>(startDate, endDate))
                                   .ReturnsAsync(new List<ICustomer>());

            // Act
            await _customerService.GetQuietCustomersAsync<Customer145>(startDate, endDate);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.GetQuietCustomersAsync<Customer145>(startDate, endDate), Times.Once);
        }

        [Fact]
        public async Task GetQuietCustomersAsync_ReturnsCorrectData()
        {
            // Arrange
            var startDate = new DateTime(2024, 4, 1);
            var endDate = new DateTime(2024, 4, 30);
            var customers = new List<ICustomer> { new Mock<ICustomer>().Object };
            _customerRepositoryMock.Setup(repo => repo.GetQuietCustomersAsync<Customer145>(startDate, endDate))
                                   .ReturnsAsync(customers);

            // Act
            var result = await _customerService.GetQuietCustomersAsync<Customer145>(startDate, endDate);

            // Assert
            Assert.Equal(customers, result);
        }
    }
}
