using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationMachineApp.Core.Models
{
    [Table("Events_2")]
    public class Event2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public int EventType { get; set; }
    }
}
