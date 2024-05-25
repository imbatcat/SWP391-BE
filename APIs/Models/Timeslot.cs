using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetHealthcareSystem.Models
{
    public class TimeSlot
    {
        [Key]
        public int TimeSlotId { get; set; }

        [DataType(DataType.Time)]
        [Required]
        public TimeOnly StartTime { get; set; } = new TimeOnly();

        [DataType(DataType.Time)]
        [Required]
        public TimeOnly EndTime { get; set; } = new TimeOnly();

    }
}
