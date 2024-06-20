using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IServicePaymentService
    {
        Task<IEnumerable<ServicePayment>> GetAll();
        Task<ServicePayment?> GetServicePaymentByCondition(Expression<Func<ServicePayment, bool>> expression);
        Task CreateServicePayment(ServicePaymentDTO entity);
        Task UpdateServicePayment(string id, ServicePaymentDTO entity);
        void DeleteServicePayment(ServicePayment entity);
    }
}
