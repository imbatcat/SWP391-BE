using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.Models
{
    public class Veterinarian : Account
    {
        public string Prefix { get; } = "VT";

        [DataType(DataType.ImageUrl)]
        public string ImgUrl { get; set; }
        [Required]
        public int Experience { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(30)]
        public string Position { get; set; }

        [StringLength(50)]
        public string Department { get; set; }
        // Reference entities
    }
}
