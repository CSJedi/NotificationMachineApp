using NotificationMachineApp.Core.Models.Interfaces;

namespace NotificationMachineApp.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<List<ICustomer>> GetQuietCustomersAsync<T>(DateTime startDate, DateTime endDate) where T : class, ICustomer;
    }
}
