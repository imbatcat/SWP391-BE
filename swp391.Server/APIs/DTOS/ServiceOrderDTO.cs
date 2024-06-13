namespace PetHealthcare.Server.APIs.DTOS
{
    public class ServiceOrderDTO
    {
        public DateOnly OrderDate { get; set; }
        
        public List<int>? ServiceId {  get; set; }
        public string MedicalRecordId {  get; set; }
    }
}
