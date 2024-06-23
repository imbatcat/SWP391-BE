namespace PetHealthcare.Server.Core.DTOS
{
    public class AdmissionRecordRegisterDTO
    {
        public DateOnly DischargeDate { get; set; }

        public string? PetCurrentCondition { get; set; }

        public bool IsDischarged { get; set; }

        public required string? PetId { get; set; }

        public required int? CageId { get; set; }

        public required string? MedicalRecordId { get; set; }

        public required string? VeterinarianId { get; set; }
    }
}
