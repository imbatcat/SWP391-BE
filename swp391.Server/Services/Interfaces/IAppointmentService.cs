using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointment();
        Task<Appointment?> GetAppointmentByCondition(Expression<Func<Appointment, bool>> expression);
        Task CreateAppointment(AppointmentDTO appointment, string id);
        Task UpdateAppointment(string id, AppointmentDTO appointment);
        void DeleteAppointment(Appointment appointment);
        Task<IEnumerable<ResAppListForCustomer>> getAllCustomerAppList(string id);
        Task<IEnumerable<ResAppListForCustomer>> getAllCustomerAppHistory(string id);
        Task<IEnumerable<ResAppListForCustomer>> SortAppointmentByDate(string id, string SortList, string SortOrder);
        bool isVetIdValid(string id);
        string GenerateId();
    }
}
