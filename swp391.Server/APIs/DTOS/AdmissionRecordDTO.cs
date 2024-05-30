namespace PetHealthcare.Server.APIs.DTOS
{
    public class AdmissionRecordDTO
    {
        public string AdmissionId { get; set; }

        public DateOnly AdmissionDate { get; set; }

        public DateOnly DischargeDate { get; set; }

        public string? PetCurrentCondition { get; set; }

        public bool IsDischarged { get; set; }

        public string PetId { get; set; }

        public int CageId { get; set; }
    }
}