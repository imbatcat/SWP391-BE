using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.Models
{
    [Index(nameof(CageNumber), IsUnique = true)]
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
