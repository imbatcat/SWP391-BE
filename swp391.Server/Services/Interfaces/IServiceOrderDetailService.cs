using PetHealthcare.Server.Core.DTOS.ServiceOrderDTOs;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IServiceOrderDetailService
    {
        Task<IEnumerable<ServiceOrderDetailDTO>> getAllServieOrderDetailByServiceOrderId(string serviceId);
        Task<IEnumerable<ServiceOrderDetailDTO>> getAllServieOrderDetail();
    }
}
