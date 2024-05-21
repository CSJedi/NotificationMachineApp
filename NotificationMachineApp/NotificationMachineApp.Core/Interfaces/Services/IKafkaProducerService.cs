namespace NotificationMachineApp.Core.Interfaces.Services
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync<T>(string topic, T message);
    }
}
