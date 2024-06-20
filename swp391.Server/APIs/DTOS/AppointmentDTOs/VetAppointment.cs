namespace PetHealthcare.Server.APIs.DTOS.AppointmentDTOs
{
    public class VetAppointment
    {
        public string AppointmentId { get; set; }
        public string OwnerName { get; set; }
        public string PetName { get; set; }
        public string TimeSlot { get; set; }
        public string PetType { get; set; }
        public string PetBreed {  get; set; }
        public string status { get; set; }
    }
}
