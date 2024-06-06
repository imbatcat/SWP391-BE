using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Identity.Client;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOrderController : ControllerBase
    {
        private readonly IServiceOrderService _serviceOrderService;
        private readonly IHealthService _healthService;
        public ServiceOrderController(IServiceOrderService _serviceOrder, IHealthService _healthService)
        {
            this._serviceOrderService = _serviceOrder;
            this._healthService = _healthService;
        }

        [HttpPost]
        public async Task<IActionResult> createServiceOrder([FromBody]ServiceOrderDTO serviceOrderDTO)
        {
            if(serviceOrderDTO.ServiceId == null)
            {
                return BadRequest(new { message = "ServiceId is null" });
            }
            foreach(int serviceId in serviceOrderDTO.ServiceId) 
            {
                if (_healthService.GetHealthServiceByCondition(h => h.ServiceId == serviceId) == null)
                {
                    return BadRequest(new { message = "ServiceId not found" });
                }
            }
            await _serviceOrderService.CreateServiceOrder(serviceOrderDTO);
            return Ok();
        }

    }
}
