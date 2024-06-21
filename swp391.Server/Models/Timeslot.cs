using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.Models
{
    public class TimeSlot
    {
        [Key]
        public int TimeSlotId { get; set; }

        [DataType(DataType.Time)]
        [Required]
        public TimeOnly StartTime { get; set; }

        [DataType(DataType.Time)]
        [Required]
        public TimeOnly EndTime { get; set; }

    }
}
