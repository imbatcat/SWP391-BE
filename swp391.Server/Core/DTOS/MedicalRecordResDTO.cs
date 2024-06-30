using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PetHealthcare.Server.Core.DTOS
{
    public class MedicalRecordResDTO
    {
        public string PetId { get; set; }

        public string AppointmentId { get; set; }

        public int PetWeight { get; set; }

        public string? Symptoms { get; set; }

        public string? Allergies { get; set; }

        public string? Diagnosis { get; set; }

        public string? AdditionalNotes { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? FollowUpAppointmentDate { get; set; }

        public string? FollowUpAppointmentNotes { get; set; }

        public string? DrugPrescriptions { get; set; }
    }
}
