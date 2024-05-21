namespace NotificationMachineApp.Core.Models
{
    public class NotificationMessage
    {
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public string Message { get; set; }
    }
}
