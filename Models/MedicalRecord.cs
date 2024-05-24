using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetHealthcareSystem.Models
{
    public class MedicalRecord
    {

        [Key]
        [Column(TypeName = "char(11)")]
        public string MedicalRecordId { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DateCreated { get; set; }

        public int PetWeight { get; set; }

        [StringLength(200)]
        public string? Symptoms { get; set; }

        [StringLength(200)]
        public string? Allergies { get; set; }

        [StringLength(200)]
        public string? Diagnosis { get; set; }

        [StringLength(300)]
        public string? AdditionalNotes { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        [DataType(DataType.Date)]
        public DateOnly? FollowUpAppointmentDate { get; set; }

        [StringLength(300)]
        public string? FollowUpAppointmentNotes { get; set; }

        [StringLength(500)]
        public string? DrugPrescriptions { get; set; }

        // Reference entities
        [Required]
        public Appointment Appointment { get; set; }

        //public Prescription? Prescription { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Pet Pet { get; set; }

        public virtual ICollection<ServiceOrder>? ServiceOrders { get; set; }

        public virtual ICollection<AdmissionRecord>? AdmissionRecords { get; set; }
    }
}
