using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;
using System.Diagnostics;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointment;

        public AppointmentController(IAppointmentService appointment)
        {
            _appointment = appointment;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<IEnumerable<Appointment>> GetAllAppointment()
        {
            return await _appointment.GetAllAppointment();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointmentByCondition(string id)
        {
            var appointment = await _appointment.GetAppointmentByCondition(a => a.AppointmentId.Equals(id));

            if (appointment == null)
            {
                return NotFound(new {message = "Appointment not found"});
            }

            return appointment;
        }

        [HttpGet("AppointmentList/{accountId}")]
        public async Task<IEnumerable<ResAppListForCustomer>> GetCustomerAppointmentList([FromRoute] string accountId)
        {
            return await _appointment.getAllCustomerAppList(accountId);
        }
        [HttpGet("AppointmentList/AppointmentHistory/{accountId}")]
        public async Task<IEnumerable<ResAppListForCustomer>> GetCustomerAppointmentHistory([FromRoute] string accountId)
        {
            return await _appointment.getAllCustomerAppHistory(accountId);
        }
        [HttpGet("AppointmentList/AppointmentHistory/{accountId}/{typeOfSorting}/{orderBy}")]
        public async Task<ActionResult<IEnumerable<ResAppListForCustomer>>> GetSortedListByDate(string accountId, string typeOfSorting, string orderBy)
        {
            if(!typeOfSorting.Equals("history",StringComparison.OrdinalIgnoreCase)
                &&
               !typeOfSorting.Equals("current", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new {message = "typeOfSorting must be current or history"});
            }
            if(!orderBy.Equals("asc", StringComparison.OrdinalIgnoreCase)
                &&
               !orderBy.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "orderBy must be asc or desc" });
            }
            var sortedAppointment = await _appointment.SortAppointmentByDate(accountId, typeOfSorting, orderBy);
            return Ok(sortedAppointment);
        }
        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment([FromRoute] string id, [FromBody] AppointmentDTO toUpdateAppointment)
        {
            var appointment = await _appointment.GetAppointmentByCondition(a => a.AppointmentId.Equals(id));
            if (appointment == null)
            {
                return NotFound(new {message ="Update fail, appointment not found"});
            } else if(!_appointment.isVetIdValid(toUpdateAppointment.VeterinarianAccountId)) { 
                return BadRequest(new {message = "Invalid foreign key VetId"});
            } else if(toUpdateAppointment.BookingPrice <= 0)
            {
                return BadRequest(new {message = "Price must be higher than 0"});
            }
            await _appointment.UpdateAppointment(id, toUpdateAppointment);
            return Ok(toUpdateAppointment);
        }

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] AppointmentDTO toCreateAppointment)
        {
            string id = _appointment.GenerateId();
            await _appointment.CreateAppointment(toCreateAppointment,id);
            return Ok(toCreateAppointment);
        }


        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService([FromRoute] string id)
        {
            var appointment = await _appointment.GetAppointmentByCondition(a => a.AppointmentId.Equals(id));
            if (appointment == null)
            {
                return NotFound(new { message = "Appointment not found" });
            }
            _appointment.DeleteAppointment(appointment);
            return Ok(appointment);
        }
    }
}
