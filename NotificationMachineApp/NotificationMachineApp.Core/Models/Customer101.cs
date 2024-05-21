using NotificationMachineApp.Core.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationMachineApp.Core.Models
{
    [Table("Customer_101")]
    public class Customer101 : ICustomer
    {
        [Key]
        public int Id { get; set; }

        [StringLength(int.MaxValue)]
        public string FirstName { get; set; }

        [StringLength(int.MaxValue)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        [StringLength(128)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        [StringLength(10)]
        public string Salutation { get; set; }

        [StringLength(128)]
        public string PasswordHash { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastLoginDate { get; set; }

        public string GetEmail() => Email;
        public string GetFullName() => $"{FirstName} {LastName}";
        public string GetUserId() => Id.ToString();

    }
}
