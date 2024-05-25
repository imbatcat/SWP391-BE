using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthcareSystem.Models
{
    public class Prescription
    {
        [NotMapped]
        public string Prefix { get; } = "PRE";

        [Key]
        [Column(TypeName = "char(11)")]
        public string PrescriptionId { get; set; }

        [StringLength(100)]
        public string VetFullName { get; set; }

        [StringLength(50)]
        public string PetFullName { get; set; }

        [StringLength(100)]
        public string OwnerFullName { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        [DataType(DataType.Date)]
        public DateOnly CreateDate { get; set; }


        // Reference entities
        [Column(TypeName = "char(11)")]
        public string MedicalRecordId { get; set; }

        [Required]
        public MedicalRecord MedicalRecord { get; set; }

        public virtual ICollection<Drug>? Drugs  { get; set; }
    }
}
