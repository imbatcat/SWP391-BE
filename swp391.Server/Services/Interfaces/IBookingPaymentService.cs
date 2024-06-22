using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IBookingPaymentService
    {
        public string GenerateBookingPaymentId();
        public void CreateBookingPayment(AppointmentDTO appointmentDTO, string appointmentId);
    }
}
