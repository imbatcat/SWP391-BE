using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.Server.Core.DTOS.ServiceOrderDTOs;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/service-order-detail-controller")]
    [ApiController]
    [Authorize(Roles = "Admin,Vet")]
    public class ServiceOrderDetailController : ControllerBase
    {
        
        private readonly IServiceOrderDetailService serviceOrderDetail;
        public ServiceOrderDetailController(IServiceOrderDetailService serviceOrderDetail)
        {
            this.serviceOrderDetail = serviceOrderDetail;
        }

        
        [HttpGet("get-all-service-order-detail")]
        public async Task<IEnumerable<ServiceOrderDetailDTO>> getAllServieOrderDetail()
        {
            return await serviceOrderDetail.getAllServieOrderDetail();
        }

        [HttpGet("get-all-service-order-detail-by-service-order-id/{serviceId}")]
        public async Task<IEnumerable<ServiceOrderDetailDTO>> getAllServieOrderDetailByServiceOrderId([FromRoute] string serviceId)
        {
            return await serviceOrderDetail.getAllServieOrderDetailByServiceOrderId(serviceId);
        }
    }
}
