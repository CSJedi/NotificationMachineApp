using Microsoft.Extensions.Logging;
using Moq;
using NotificationMachineApp.Core.Models;
using NotificationMachineApp.Infrastructure.Services;

namespace NotificationMachineApp.Tests.Services
{
    public class ClientCodeServiceTests
    {
        private readonly Mock<ILogger<ClientCodeService>> _loggerMock;
        private readonly ClientCodeService _clientCodeService;

        public ClientCodeServiceTests()
        {
            _loggerMock = new Mock<ILogger<ClientCodeService>>();
            _clientCodeService = new ClientCodeService(_loggerMock.Object);
        }

        [Fact]
        public async Task GenerateClientCodeAsync_ShouldReturnCorrectCode_ForCustomer145_WithFirstNameAndLastName()
        {
            // Arrange
            var customer = new Customer145 { Name = "Ira Washington", Email = "ira.washington@example.com" };
            var tenant = new Tenant { OrganisationName = "Jack Sparrow Warship" };
            var expectedCode = "AR-HSA-JSW";

            // Act
            var result = await _clientCodeService.GenerateClientCodeAsync(customer, tenant);

            // Assert
            Assert.Equal(expectedCode, result);
        }

        [Fact]
        public async Task GenerateClientCodeAsync_ShouldReturnCorrectCode_ForCustomer145_WithFirstNameOnly()
        {
            // Arrange
            var customer = new Customer145 { Name = "John", Email = "john.doe@example.com" };
            var tenant = new Tenant { OrganisationName = "Jack Sparrow Warship" };
            var expectedCode = "NHO--JSW";

            // Act
            var result = await _clientCodeService.GenerateClientCodeAsync(customer, tenant);

            // Assert
            Assert.Equal(expectedCode, result);
        }

        [Fact]
        public async Task GenerateClientCodeAsync_ShouldReturnCorrectCode_ForCustomer145_WithMultipleWordLastName()
        {
            // Arrange
            var customer = new Customer145 { Name = "Paul de Ville", Email = "paul.deville@example.com" };
            var tenant = new Tenant { OrganisationName = "Jack Sparrow Warship" };
            var expectedCode = "LUA-IVE-JSW";

            // Act
            var result = await _clientCodeService.GenerateClientCodeAsync(customer, tenant);

            // Assert
            Assert.Equal(expectedCode, result);
        }

        [Fact]
        public async Task GenerateClientCodeAsync_ShouldReturnCorrectCode_ForCustomer2()
        {
            // Arrange
            var customer = new Customer2 { GivenName = "Jack", FamilyName = "Sparrow", Email = "john.doe@example.com" };
            var tenant = new Tenant { OrganisationName = "Jack Sparrow Warship" };
            var expectedCode = "KCA-RAP-JSW";

            // Act
            var result = await _clientCodeService.GenerateClientCodeAsync(customer, tenant);

            // Assert
            Assert.Equal(expectedCode, result);
        }

        [Fact]
        public async Task GenerateClientCodeAsync_ShouldReturnCorrectCode_ForCustomer101()
        {
            // Arrange
            var customer = new Customer101 { FirstName = "Jack", LastName = "Sparrow", Email = "john.doe@example.com" };
            var tenant = new Tenant { OrganisationName = "Jack Sparrow Warship" };
            var expectedCode = "KCA-RAP-JSW";

            // Act
            var result = await _clientCodeService.GenerateClientCodeAsync(customer, tenant);

            // Assert
            Assert.Equal(expectedCode, result);
        }
    }
}
