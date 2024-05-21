using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationMachineApp.Core.Models
{
    [Table("Events_145")]
    public class Event145
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [Required]
        [StringLength(128)]
        public string CustomerId { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string EventType { get; set; } 
    }
}