using PetHealthcare.Server.Core.DTOS.ServiceOrderDTOs;
using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IServiceOrderRepository : IRepositoryBase<ServiceOrder>
    {
        Task CreateServiceOrder(ServiceOrderDTO order);
        Task UpdateServiceOrder(string ServiceOrderId, List<int> ServiceId);
        Task<IEnumerable<GetAllServiceOrderForStaff>> GetAllServiceOrderForStaff();
        Task<bool> UpdateServiceOrderStatus(string serviceOrderId);
    }
}
