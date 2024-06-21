namespace PetHealthcare.Server.Core.DTOS.AppointmentDTOs
{
    public class AppointmentForVetDTO
    {
        public string CustomerPhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string PetName { get; set; }
        public string AppointmentNotes { get; set; }
        public string Status { get; set; }
    }
}
