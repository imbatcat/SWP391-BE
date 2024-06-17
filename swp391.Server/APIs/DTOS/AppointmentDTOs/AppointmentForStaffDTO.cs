namespace PetHealthcare.Server.APIs.DTOS.AppointmentDTOs
{
    public class AppointmentForStaffDTO
    {
        public string appointmentId { get; set; }
        public string customerName { get; set; }
        public string phoneNumber { get; set; }
        public string petName { get; set; }
        public string status { get; set; }

        public string VetName { get; set; }

    }
}
