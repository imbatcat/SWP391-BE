using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PetHealthcare.Server.APIs.DTOS.AppointmentDTOs
{
    public class CreateAppointmentDTO
    {
        public DateOnly AppointmentDate { get; set; }

        [StringLength(50)]
        public string AppointmentType { get; set; }

        [StringLength(200)]
        public string? AppointmentNotes { get; set; }

        public double BookingPrice { get; set; }

        public string AccountId { get; set; }
        public string PetId { get; set; }
        public string VeterinarianAccountId { get; set; }
        public int TimeSlotId { get; set; }
        public virtual ICollection<BookingPayment> BookingPayments { get; set; }

    }
}
