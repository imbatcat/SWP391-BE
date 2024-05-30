using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public string GenerateId()
        {
            var prefix = "AP-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;
        }
        public async Task CreateAppointment(AppointmentDTO appointment)
        {
            Appointment toCreateAppointment = new Appointment
            {
                AppointmentType = appointment.AppointmentType,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentNotes = appointment.AppointmentNotes,
                BookingPrice = appointment.BookingPrice,
                PetId = appointment.PetId,
                VeterinarianAccountId = appointment.VeterinarianAccountId,
                AppointmentId = GenerateId(),
                AccountId = appointment.AccountId,
                TimeSlotId = appointment.TimeSlotId
            };
            await _appointmentRepository.Create(toCreateAppointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _appointmentRepository.Delete(appointment);
        }



        public async Task<IEnumerable<Appointment>> GetAllAppointment()
        {
            return await _appointmentRepository.GetAll();
        }

        public async Task<Appointment?> GetAppointmentByCondition(Expression<Func<Appointment, bool>> expression)
        {
            return await _appointmentRepository.GetByCondition(expression);
        }

        public async Task UpdateAppointment(string id, AppointmentDTO appointment)
        {
            Appointment UpdateAppointment = new Appointment
            {
                AppointmentDate = appointment.AppointmentDate,
                AppointmentNotes = appointment.AppointmentNotes,
                BookingPrice = appointment.BookingPrice,
                PetId = appointment.PetId,
                AppointmentId = id,
                VeterinarianAccountId = appointment.VeterinarianAccountId,
                TimeSlotId = appointment.TimeSlotId,
                AppointmentType = appointment.AppointmentType,
            };
            await _appointmentRepository.Update(UpdateAppointment);
        }
    }
}
