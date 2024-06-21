namespace PetHealthcare.Server.Core.DTOS.AppointmentDTOs
{
    public class CustomerAppointmentDTO
    {
        public DateOnly AppointmentDate { get; set; }
        public string? AppointmentNotes { get; set; }
        public string VeterinarianAccountId { get; set; }
        public int TimeSlotId { get; set; }

    }
}
