using Microsoft.AspNetCore.Mvc;
using NotificationMachineApp.Core.Interfaces.Services;
using NotificationMachineApp.Core.Models;

namespace NotificationMachineApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TriggerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly INotificationService _notificationService;

        private static readonly DateTime StartDate = new DateTime(2024, 4, 1);
        private static readonly DateTime EndDate = new DateTime(2024, 4, 30);

        public TriggerController(ICustomerService customerService, INotificationService notificationService)
        {
            _customerService = customerService;
            _notificationService = notificationService;
        }

        [HttpPost("kickoff")]
        public async Task<ActionResult> KickOff()
        {
            try
            {
                var quietCustomers145 = await _customerService.GetQuietCustomersAsync<Customer145>(StartDate, EndDate);
                await _notificationService.QueueNotificationTasksAsync(quietCustomers145, "Notification message for Customer145", "Topic145");

                var quietCustomers2 = await _customerService.GetQuietCustomersAsync<Customer2>(StartDate, EndDate);
                await _notificationService.QueueNotificationTasksAsync(quietCustomers2, "Notification message for Customer2", "Topic2");

                var quietCustomers101 = await _customerService.GetQuietCustomersAsync<Customer101>(StartDate, EndDate);
                await _notificationService.QueueNotificationTasksAsync(quietCustomers101, "Notification message for Customer101", "Topic101");

                return Ok("Notification tasks queued successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
