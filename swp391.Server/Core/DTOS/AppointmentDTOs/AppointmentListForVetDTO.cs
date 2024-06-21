namespace PetHealthcare.Server.Core.DTOS.AppointmentDTOs
{
    public class AppointmentListForVetDTO
    {
        public string CustomerName { get; set; }
        public string PetName { get; set; }
        public string AppointmentNotes { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public string CustomerPhone { get; set; }

    }
}
