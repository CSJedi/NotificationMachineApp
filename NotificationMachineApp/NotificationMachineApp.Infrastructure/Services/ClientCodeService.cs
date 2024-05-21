using Microsoft.Extensions.Logging;
using NotificationMachineApp.Core.Interfaces.Services;
using NotificationMachineApp.Core.Models;
using NotificationMachineApp.Core.Models.Interfaces;

namespace NotificationMachineApp.Infrastructure.Services
{
    public class ClientCodeService : IClientCodeService
    {
        private readonly ILogger<ClientCodeService> _logger;

        public ClientCodeService(ILogger<ClientCodeService> logger)
        {
            _logger = logger;
        }

        public Task<string> GenerateClientCodeAsync(ICustomer customer, Tenant tenant)
        {
            _logger.LogInformation($"Generating client code for customer with Email {customer.GetEmail()}...");
            try
            {
                var names = customer.GetFullName().Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                var firstName = names[0];
                var lastName = names.Length > 1 ? names[1] : "";

                var part1 = ProcessNamePart(firstName);
                var part2 = ProcessNamePart(lastName);
                var part3 = string.Concat(tenant.OrganisationName.Split(' ').Select(word => word[0])).ToUpper();

                var clientCode = $"{part1}-{part2}-{part3}";
                _logger.LogInformation($"Generated client code: {clientCode}");
                return Task.FromResult(clientCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating client code.");
                throw;
            }
        }

        private string ProcessNamePart(string namePart)
        {
            return new string(namePart.Replace(" ", "").Skip(1).Take(3).Reverse().ToArray()).ToUpper();
        }
    }
}
