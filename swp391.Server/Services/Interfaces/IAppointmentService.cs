using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<GetAllAppointmentForAdminDTO>> GetAllAppointment();
        Task<IEnumerable<GetAllAppointmentForAdminDTO>> GetAllAppointmentByAccountId(string acId);
        Task<Appointment?> GetAppointmentByCondition(Expression<Func<Appointment, bool>> expression);
        Task CreateAppointment(CreateAppointmentDTO appointment, string id);
        Task UpdateAppointment(string id, CustomerAppointmentDTO appointment);
        void DeleteAppointment(Appointment appointment);
        Task<IEnumerable<ResAppListForCustomer>> getAllCustomerAppointment(string id, string listType);
        Task<IEnumerable<ResAppListForCustomer>> SortAppointmentByDate(string id, string SortList, string SortOrder);
        bool isVetIdValid(string id);
        string GenerateId();

        Task<Account?> GetAccountById(string id);
    }
}
