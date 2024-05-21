using NotificationMachineApp.Core.Models.Interfaces;

namespace NotificationMachineApp.Core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<ICustomer>> GetQuietCustomersAsync<T>(DateTime startDate, DateTime endDate) where T : class, ICustomer;
    }
}
