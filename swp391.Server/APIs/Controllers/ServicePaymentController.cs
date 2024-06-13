using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.APIs.Controllers
{
    public interface ServicePaymentController
    {
        [Route("api/[controller]")]
        [Authorize(Roles = "Veterianrian, Staff, User, Admin")]
        [ApiController]
        public class ServicePaymentController : ControllerBase
        {
            private readonly IServicePaymentService _context;

            public ServicePaymentController(IServicePaymentService context)
            {
                _context = context;
            }

            // GET: api/Accounts
            [HttpGet("")]
            [ProducesResponseType(200, Type = typeof(IEnumerable<ServicePayment>))]
            public async Task<IEnumerable<ServicePayment>> GetServicePayments()
            {
                return await _context.GetAll();
            }

            // GET: api/Services/5
            [HttpGet("{id}")]
            public async Task<ActionResult<ServicePayment>> GetServicePaymentByCondition(string id)
            {
                var service = await _context.GetServicePaymentByCondition(p => p.ServicePaymentId == id);

                if (service == null)
                {
                    return NotFound();
                }

                return service;
            }

            // PUT: api/Services/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            [Authorize(Roles = "Staff,Admin")]
            public async Task<IActionResult> UpdateTimeSlot([FromRoute] string id, [FromBody] ServicePaymentDTO toUpdate)
            {
                var service = await _context.GetServicePaymentByCondition(p => p.ServicePaymentId == id);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _context.UpdateServicePayment(id, toUpdate);
                return Ok(toUpdate);
            }

            // POST api/<Controller>
            [HttpPost]
            [Authorize(Roles = "Staff,Admin")]
            public async Task<ActionResult<TimeSlot>> Post([FromBody] ServicePaymentDTO newCage)
            {
                await _context.CreateServicePayment(newCage);

                return CreatedAtAction(nameof(Post), newCage.GetHashCode(), newCage);
            }
        }
    }
}
