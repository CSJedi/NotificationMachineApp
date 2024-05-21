using NotificationMachineApp.Core.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationMachineApp.Core.Models
{
    [Table("Customer_2")]
    public class Customer2 : ICustomer
    {
        [Key]
        public int Id { get; set; }

        [StringLength(int.MaxValue)]
        public string GivenName { get; set; }

        [StringLength(int.MaxValue)]
        public string FamilyName { get; set; }

        [StringLength(128)]
        public string JobPosition { get; set; }

        [EmailAddress]
        [StringLength(128)]
        public string Email { get; set; }

        [StringLength(128)]
        public string PasswordHash { get; set; }

        public string GetEmail() => Email;
        public string GetFullName() => $"{GivenName} {FamilyName}";
        public string GetUserId() => Id.ToString();
    }
}
