using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.Models
{
    public class Cage
    {
        [Key]
        public int CageId { get; set; }

        public int CageNumber { get; set; }

        public bool IsOccupied { get; set; }

        // Reference entities
        public virtual ICollection<AdmissionRecord> AdmissionRecords { get; set; }
    }
}
