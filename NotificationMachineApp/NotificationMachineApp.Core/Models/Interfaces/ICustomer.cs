namespace NotificationMachineApp.Core.Models.Interfaces
{
    public interface ICustomer
    {
        string GetEmail();
        string GetFullName();
        string GetUserId();
    }
}
