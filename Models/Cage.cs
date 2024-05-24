using System.ComponentModel.DataAnnotations;

namespace PetHealthcareSystem.Models
{
    public class Cage 
    {
        [Key]
        public int CageId { get; set; }

        public int CageNumber { get; set; }

        public bool IsOccupied { get; set; }

        // Reference entities
        public ICollection<AdmissionRecord> AdmissionRecords{ get; set; }
    }
}
