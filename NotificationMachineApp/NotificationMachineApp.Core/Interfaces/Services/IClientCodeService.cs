using NotificationMachineApp.Core.Models;
using NotificationMachineApp.Core.Models.Interfaces;

namespace NotificationMachineApp.Core.Interfaces.Services
{
    public interface IClientCodeService
    {
        Task<string> GenerateClientCodeAsync(ICustomer customer, Tenant tenant);
    }
}
