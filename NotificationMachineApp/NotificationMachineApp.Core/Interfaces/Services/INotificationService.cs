using NotificationMachineApp.Core.Models.Interfaces;

namespace NotificationMachineApp.Core.Interfaces.Services
{
    public interface INotificationService
    {
        Task QueueNotificationTasksAsync(IEnumerable<ICustomer> customers, string message, string topic);
    }
}
