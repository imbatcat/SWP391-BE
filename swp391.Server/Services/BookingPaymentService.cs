using NanoidDotNet;
using PetHealthcare.Server.Core.DTOS.AppointmentDTOs;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.Services
{
    public class BookingPaymentService : IBookingPaymentService
    {
        private readonly IBookingPaymentRepository _bookingPaymentRepo;
        public BookingPaymentService(IBookingPaymentRepository bookingPaymentRepo)
        {
            _bookingPaymentRepo = bookingPaymentRepo;
        }

        public void CreateBookingPayment(AppointmentDTO appointmentDTO, string appointmentId)
        {
            throw new NotImplementedException();
        }

        public string GenerateBookingPaymentId()
        {
            string prefix = "BP-";
            return prefix + Nanoid.Generate(size: 8);
        }
    }
}
