using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthcare.Server.Models
{
    public class PetHealthTracker
    {
        [NotMapped]
        public string Prefix { get; } = "PT";

        [Column(TypeName = "char(11)")]
        public string PetHealthTrackerId { get; set; }

        [StringLength(50)]
        public string PetName { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        [DataType(DataType.Date)]
        public DateOnly TrackerDate { get; set; }

        public string Description { get; set; }

        // Reference entities
        [Required]
        public Pet Pet { get; set; }

        //[Required]
        //public AdmissionRecord AdmissionRecord { get; set; }
    }
}
