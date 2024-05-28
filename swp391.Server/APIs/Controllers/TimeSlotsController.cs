using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotsController : ControllerBase
    {
        private readonly ITimeSlotService _context;

        public TimeSlotsController(ITimeSlotService context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TimeSlot>))]
        public async Task<IEnumerable<TimeSlot>> GetTimeSlots()
        {
            return await _context.GetAllTimeSlots();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeSlot>> GetTimeSlotByCondition(int id)
        {
            var service = await _context.GetTimeSlotByCondition(p => p.TimeSlotId == id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeSlot([FromRoute] int id, [FromBody] TimeslotDTO toUpdateTimeSlot)
        {
            var service = await _context.GetTimeSlotByCondition(p => p.TimeSlotId == id);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _context.UpdateTimeSlot(id, toUpdateTimeSlot);
            return Ok(toUpdateTimeSlot);
        }
    }
}
