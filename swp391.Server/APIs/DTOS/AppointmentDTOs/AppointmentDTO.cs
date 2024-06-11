namespace PetHealthcare.Server.APIs.DTOS.AppointmentDTOs
{
    public class AppointmentDTO
    {
        public DateOnly AppointmentDate { get; set; }
        public string AppointmentType { get; set; }
        public string? AppointmentNotes { get; set; }
        public double BookingPrice { get; set; }
        public string PetId { get; set; }
        public string AccountId { get; set; }
        public string VeterinarianAccountId { get; set; }

        public int TimeSlotId { get; set; }

    }
}
