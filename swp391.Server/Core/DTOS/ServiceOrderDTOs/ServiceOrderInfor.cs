namespace PetHealthcare.Server.Core.DTOS.ServiceOrderDTOs
{
    public class ServiceOrderInfor
    {
        public string ServiceOrderId {  get; set; }
        public string OwnerName { get; set; }
        public string PetName {  get; set; }
        public int PetAge {  get; set; }
        public string PetGender {  get; set; }
        public DateOnly CreatedDate { get; set; }
        public string diagnosis { get; set; }
    }
}
