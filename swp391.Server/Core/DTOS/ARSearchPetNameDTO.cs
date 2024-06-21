namespace PetHealthcare.Server.Core.DTOS
{
    public class ARSearchPetNameDTO
    {
        public string AdmissionId { get; set; }
        public DateOnly? AdmissionDate { get; set; }
        public DateOnly? DischargeDate { get; set; }
        public bool IsDischarged { get; set; }
        public string? PetCurrentCondition { get; set; }
        public string? MedicalRecordId { get; set; }
        public string? VeterinarianAccountId { get; set; }
        public string? PetId { get; set; }
        public int CageId { get; set; }

        public ARSearchPetNameDTO(string one, DateOnly? two, DateOnly? three, bool four, string? five, string? six, string? seven, string? eight, int nine)
        {
            AdmissionId = one;
            AdmissionDate = two;
            DischargeDate = three;
            IsDischarged = four;
            PetCurrentCondition = five;
            MedicalRecordId = six;
            VeterinarianAccountId = seven;
            PetId = eight;
            CageId = nine;
        }
    }

}
