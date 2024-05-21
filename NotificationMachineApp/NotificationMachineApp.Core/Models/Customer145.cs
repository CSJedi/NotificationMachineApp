using NotificationMachineApp.Core.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationMachineApp.Core.Models
{
    [Table("Customer_145")]
    public class Customer145 : ICustomer
    {
        [Key]
        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(128)]
        public string Email { get; set; }

        [StringLength(128)]
        public string Password { get; set; }

        public string GetEmail() => Email;
        public string GetFullName() => Name;
        public string GetUserId() => UserId;
    }
}
