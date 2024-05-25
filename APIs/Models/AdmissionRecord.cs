using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthcareSystem.Models
{
    public class AdmissionRecord
    {
        [NotMapped]
        public string Prefix { get; } = "AR";

        [Key]
        [Column(TypeName = "char(11)")]
        [MaxLength(10)]
        public string AdmissionId { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        [DataType(DataType.Date)]
        public DateOnly AdmissionDate { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        [DataType(DataType.Date)]
        [DefaultValue(null)]
        public DateOnly? DischargeDate { get; set; }

        [DefaultValue(false)]
        public bool IsDischarged { get; set; }

        [StringLength(50)]
        public string? PetCurrentCondition { get; set; }

        // Reference entities
        //public virtual ICollection<PetHealthTracker> PetHealthTrackers { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public MedicalRecord MedicalRecord { get; set; }

        [Required]
        public Veterinarian Veterinarian { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Pet Pet { get; set; }

        [Required]
        public Cage Cage { get; set; }  
    }
}
