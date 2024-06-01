namespace PetHealthcare.Server.APIs.DTOS
{
    public class MedicalRecordDTO
    {
        public string MedicalRecordId { get; set; }
        public DateOnly DataCreated { get; set; }
        public int PetWeight { get; set; }
        public string? Symptoms { get; set; }
        public string? Allergies { get; set; }
        public string? Diagnosis { get; set; }
        public string? AdditionallNotes { get; set; }
        public DateTime? FollowUpAppointmentDate { get; set; }
        public string? FollowUpAppointmentNotes { get; set; }
        public string? DrugPrescription { get; set; }
        public string AppointmentId { get; set; }
        public string PetId { get; set; }
    }
}
