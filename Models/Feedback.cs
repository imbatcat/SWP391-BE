using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHealthcareSystem.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [Range(0, 5)]
        [DefaultValue(0)]
        public int Ratings { get; set; }

        [StringLength(250)]
        public string? FeedbackDetails { get; set; }

        public Account? Account { get; set; }

    }
}
