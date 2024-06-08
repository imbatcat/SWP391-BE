using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PetHealthcare.Server.APIs.DTOS.AppointmentDTOs
{
    public class GetAllAppointmentDTOs
    {
        public string AppointmentId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public string AppointmentType { get; set; }
        public string? AppointmentNotes { get; set; }
        public double BookingPrice { get; set; }
        public string AccountId { get; set; }
        public string PetName { get; set; }
        public string VeterinarianName { get; set; }
        public int TimeSlotId { get; set; }
        public bool IsCancel { get; set; }
        public bool IsCheckIn { get; set; }

    }
}
