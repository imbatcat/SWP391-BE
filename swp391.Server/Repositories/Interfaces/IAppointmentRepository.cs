using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IAppointmentRepository : IRepositoryBase<Appointment>
    {
        bool isInputtedVetIdValid(string id);
        Task<IEnumerable<Appointment>> GetAppointmentsOfWeek(DateOnly startWeekDate, DateOnly endWeekDate);

        Task<Account?> GetAccountById(string id);
    }
}
