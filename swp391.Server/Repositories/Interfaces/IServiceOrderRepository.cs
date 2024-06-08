using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.ServiceOrderDTO;
using PetHealthcare.Server.APIs.DTOS.ServiceOrderDTOs;
using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IServiceOrderRepository: IRepositoryBase<ServiceOrder>
    {
        Task CreateServiceOrder(ServiceOrderDTO order);
        Task UpdateServiceOrder(string ServiceOrderId, List<int> ServiceId);
        Task<IEnumerable<GetAllServiceOrderForStaff>> GetAllServiceOrderForStaff();
    }
}
