using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using PetHealthcareSystem.APIs.DTOS;
using PetHealthcareSystem.Models;
using PetHealthcareSystem.Services;

namespace PetHealthcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly HealthService _healthService;

        public ServicesController(HealthService healthService)
        {
            _healthService = healthService;
        }

        // GET: api/Services
        [HttpGet]
        public IEnumerable<Service> GetService()
        {
            return _healthService.GetAllHealthService();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public ActionResult<Service> GetServiceByCondition(int id)
        {
            var service = _healthService.GetHealthServiceByCondition(s => s.ServiceId == id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult UpdateService([FromRoute] int id, [FromBody] HealthServiceDTO toUpdateService)
        {
            var service = _healthService.GetHealthServiceByCondition(s => s.ServiceId == id);
            if (service == null)
            {
                return BadRequest();
            }
            _healthService.UpdateHealthService(id, toUpdateService);
            return Ok(toUpdateService);
        }

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Service> CreateService([FromBody] HealthServiceDTO toUpdateService)
        {
            _healthService.CreateHealthService(toUpdateService);

            return Ok(toUpdateService);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public IActionResult DeleteService([FromRoute] int id)
        {
            var toDeleteService = _healthService.GetHealthServiceByCondition(s => s.ServiceId == id);
            if(toDeleteService == null)
            {
                return NotFound(new {message ="Service not found"});
            }
            _healthService.DeleteHealthService(toDeleteService);
            return Ok(toDeleteService);
        }
    }
}
