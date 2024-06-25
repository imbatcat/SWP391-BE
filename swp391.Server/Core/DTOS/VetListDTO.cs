namespace PetHealthcare.Server.Core.DTOS
{
    public class VetListDTO
    {
        public string VetId {  get; set; }
        public string VetName { get; set; }
        public string Department { get; set; }
        public int Experience { get; set; }
        public string TimeSlot { get; set; }
        public string CurrentCapacity { get; set; }
    }
}
