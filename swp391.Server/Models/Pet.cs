using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHealthcare.Server.Models
{
    public class Pet
    {
        [NotMapped]
        public string Prefix { get; } = "PE";

        [Key]
        [Column(TypeName = "char(11)")]
        public string PetId { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImgUrl { get; set; }

        [StringLength(50)]
        [Required]
        public string PetName { get; set; }

        [StringLength(50)]
        [Required]
        public string PetBreed { get; set; }

        [Required]
        public int PetAge { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [DefaultValue(true)]
        [Required]
        public bool IsMale { get; set; }

        [DefaultValue(true)]
        [Required]
        public bool IsCat { get; set; }

        [DefaultValue("")]
        [StringLength(200)]
        public string? VaccinationHistory { get; set; }

        [DefaultValue(false)]
        public bool IsDisabled { get; set; }


        // Reference entity
        [Required]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Account Account { get; set; }
        public string AccountId { get;  set; }
        public virtual ICollection<AdmissionRecord> AdmissionRecords { get; set; }
    }
}

