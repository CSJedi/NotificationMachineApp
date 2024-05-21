namespace NotificationMachineApp.Core.Interfaces.Services
{
    public interface IKafkaConsumerService
    {
        Task ConsumeAsync(string topic);
    }
}
