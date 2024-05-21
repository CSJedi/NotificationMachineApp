using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationMachineApp.Core.Models
{
    [Table("Events_101")]
    public class Event101
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string EventType { get; set; }
    }
}
