using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHealthcare.Server.Models
{
    public class Drug
    {
        [NotMapped]
        public string Prefix { get; } = "DRG";

        [Key]
        [Column(TypeName = "char(11)")]
        public string DrugId { get; set; }

        [StringLength(100)]
        public string DrugInfo { get; set; }

        [StringLength(200)]
        public string DrugGuide { get; set; }

        [StringLength(50)]
        public string DrugName { get; set; }

        // Reference entities
        public ICollection<Prescription>? Prescriptions { get; set; }

    }
}
