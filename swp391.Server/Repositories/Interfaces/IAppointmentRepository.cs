using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IAppointmentRepository : IRepositoryBase<Appointment>
    {
        bool isInputtedVetIdValid(string id);
        Task<Account?> GetAccountById(string id);

        Task<IEnumerable<Appointment>> GetAllAppointmentListForVet(string vetId, DateOnly date);
        Task<IEnumerable<Appointment>> GetVetAppointmentList (string vetId, int timeSlot, DateOnly date);

        Task<IEnumerable<Appointment>> GetAllAppointmentForStaff(DateOnly date, int timeslot);

 
    }
}
