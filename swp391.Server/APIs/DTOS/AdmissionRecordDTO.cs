namespace PetHealthcare.Server.APIs.DTOS
{
    public class AdmissionRecordDTO
    {

        public DateOnly DischargeDate { get; set; }

        public string? PetCurrentCondition { get; set; }

        public bool IsDischarged { get; set; }

    }
}