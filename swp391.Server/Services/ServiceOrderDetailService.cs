using PetHealthcare.Server.Core.DTOS.ServiceOrderDTOs;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class ServiceOrderDetailService : IServiceOrderDetailService
    {
        private readonly IServiceOrderDetailRepository _repository;
        public ServiceOrderDetailService(IServiceOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceOrderDetailDTO>> getAllServieOrderDetail()
        {
            IEnumerable<ServiceOrderDetails> orDetailList = await _repository.getAllServieOrderDetail();
            List<ServiceOrderDetailDTO> serviceOrderDetailList = new List<ServiceOrderDetailDTO>();
            foreach (ServiceOrderDetails detail in orDetailList)
            {
                serviceOrderDetailList.Add(new ServiceOrderDetailDTO
                {
                    OrderId = detail.ServiceOrderId,
                    ServiceName = detail.Service.ServiceName,
                    Price = detail.Service.ServicePrice,
                });
            }
            return serviceOrderDetailList;
        }

        public async Task<IEnumerable<ServiceOrderDetailDTO>> getAllServieOrderDetailByServiceOrderId(string serviceId)
        {
            IEnumerable<ServiceOrderDetails> orDetailList = await _repository.getAllServieOrderDetail();
            List<ServiceOrderDetailDTO> serviceOrderDetailList = new List<ServiceOrderDetailDTO>();
            foreach (ServiceOrderDetails detail in orDetailList)
            {
                if (detail.ServiceOrderId.Equals(serviceId, StringComparison.OrdinalIgnoreCase))
                {
                    serviceOrderDetailList.Add(new ServiceOrderDetailDTO
                    {
                        OrderId = detail.ServiceOrderId,
                        ServiceName = detail.Service.ServiceName,
                        Price = detail.Service.ServicePrice,
                    });
                }
            }
            return serviceOrderDetailList;
        }
    }
}
