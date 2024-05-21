using Microsoft.Extensions.Logging;
using NotificationMachineApp.Core.Interfaces.Repositories;
using NotificationMachineApp.Core.Interfaces.Services;
using NotificationMachineApp.Core.Models.Interfaces;

namespace NotificationMachineApp.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<List<ICustomer>> GetQuietCustomersAsync<T>(DateTime startDate, DateTime endDate) where T : class, ICustomer
        {
            _logger.LogInformation($"Fetching quiet customers of type {typeof(T).Name}...");
            return await _customerRepository.GetQuietCustomersAsync<T>(startDate, endDate);
        }
    }
}
