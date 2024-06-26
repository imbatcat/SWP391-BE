namespace PetHealthcare.Server.Core.DTOS
{
    public class MedicalRecordVetDTO
    {
        public int PetWeight { get; set; }

        public string? Symptoms { get; set; }

        public string? Allergies { get; set; }

        public string? Diagnosis { get; set; }

        public string? AdditionalNotes { get; set; }

        public DateOnly? FollowUpAppointmentDate { get; set; }

        public string? FollowUpAppointmentNotes { get; set; }

        public string? DrugPrescriptions { get; set; }
    }
}
