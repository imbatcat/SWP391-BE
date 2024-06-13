using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;
using System.Data;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Staff,Admin,Customer,Vet")]

    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointment;

        public AppointmentController(IAppointmentService appointment)
        {
            _appointment = appointment;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<IEnumerable<GetAllAppointmentForAdminDTO>> GetAllAppointment()
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
        [HttpGet("admin/{accountId}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<GetAllAppointmentForAdminDTO>> GetAllAppointmentForAdminByAccountId([FromRoute]string accountId)
        {
            if(accountId == null)
            {
                return BadRequest(new { message = "Account id must not null" });
            }
            var appointmentList = await _appointment.GetAllAppointmentByAccountId(accountId);
            if (appointmentList.Count() == 0)
            {
                return NotFound(new { message = "Can't find that account id or Account don't have any appointment" });
            }
            return Ok(appointmentList);
        }

        [HttpGet("AppointmentList/{accountId}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<ActionResult<IEnumerable<ResAppListForCustomer>>> GetCustomerAppointmentList([FromRoute] string accountId, string listType)
        {
            if (!listType.Equals("history", StringComparison.OrdinalIgnoreCase)
                &&
               !listType.Equals("current", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "listType must be current or history" });
            }
            var appointmentList = await _appointment.getAllCustomerAppointment(accountId,listType);
            if(appointmentList.Count() == 0)
            {
                return NotFound(new {message="Can't find that account id or Account don't have any appointment yet"});
            }
            return Ok(appointmentList);
        }

        [HttpGet("AppointmentList/{accountId}&{typeOfSorting}&{orderBy}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<ActionResult<IEnumerable<ResAppListForCustomer>>> GetSortedListByDate(string accountId, string typeOfSorting, string orderBy="asc")
        {
            if (!typeOfSorting.Equals("history", StringComparison.OrdinalIgnoreCase)
                &&
               !typeOfSorting.Equals("current", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "typeOfSorting must be current or history" });
            }
            if (!orderBy.Equals("asc", StringComparison.OrdinalIgnoreCase)
                &&
               !orderBy.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "orderBy must be asc or desc" });
            }
            var sortedAppointment = await _appointment.SortAppointmentByDate(accountId, typeOfSorting, orderBy);
            if(sortedAppointment == null)
            {
                return NotFound(new { message = "Can't find that account id" });
            }
            return Ok(sortedAppointment);
        }

        // Get list of active appointments by timeslot in a week
        [Authorize(Roles = "Admin, Vet")]
        [HttpPost("AppointmentList/by-week/{startWeek}&{endWeek}")]
        public async Task<IEnumerable<AppointmentForVetDTO>> GetAppointmentsByWeek([FromRoute] DateOnly startWeek, [FromRoute] DateOnly endWeek, [FromBody] TimeslotDTO timeslot)
        {
            return await _appointment.GetAppointmentsByTimeDate(startWeek, endWeek, timeslot);
        }


        // PUT: api/Services/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment([FromRoute] string id, [FromBody] CustomerAppointmentDTO toUpdateAppointment)
        {
            var appointment = await _appointment.GetAppointmentByCondition(a => a.AppointmentId.Equals(id));
            if (appointment == null)
            {
                return NotFound(new {message ="Update fail, appointment not found"});
            } else if(!_appointment.isVetIdValid(toUpdateAppointment.VeterinarianAccountId)) { 
                return BadRequest(new {message = "Invalid foreign key VetId"});
            }
            await _appointment.UpdateAppointment(id, toUpdateAppointment);
            return Ok(toUpdateAppointment);
        }

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Customer,Staff,Admin")]
        public async Task<ActionResult<GetAllAppointmentDTOs>> CreateAppointment([FromBody] CreateAppointmentDTO toCreateAppointment)
        {
            await _appointment.CreateAppointment(toCreateAppointment);
            return Ok(toCreateAppointment);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApppointment([FromRoute] string id)
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
