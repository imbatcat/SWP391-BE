using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointment();
        Task<Appointment?> GetAppointmentByCondition(Expression<Func<Appointment, bool>> expression);
        Task CreateAppointment(AppointmentDTO appointment);
        Task UpdateAppointment(string id, AppointmentDTO appointment);
        void DeleteAppointment(Appointment appointment);

        bool isVetIdValid(string id);
        string GenerateId();
    }
}
