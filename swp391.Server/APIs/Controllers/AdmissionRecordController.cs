using Microsoft.AspNetCore.Mvc;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.APIs.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionRecordController : ControllerBase
    {
        private readonly IAdmissionRecordService _context;

        public AdmissionRecordController(IAdmissionRecordService context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet("/api/GetAllAdmissionRecords")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AdmissionRecord>))]
        public async Task<IEnumerable<AdmissionRecord>> GetAllAdmissionRecords()
        {
            return await _context.GetAll();
        }

        // GET: api/Services/5
        [HttpGet("/api/GetAdmissionRecordByCondition/{id}")]
        public async Task<ActionResult<AdmissionRecord>> GetAdmissionRecordByCondition([FromRoute] string id)
        {
            var service = await _context.GetAdmissionRecordByCondition(a => a.AdmissionId == id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/api/UpdateAdmissionRecord/{id}")]
        public async Task<IActionResult> UpdateAdmissionRecord([FromRoute] string id, [FromBody] AdmissionRecordDTO toUpdate)
        {
            var service = await _context.GetAdmissionRecordByCondition(p => p.AdmissionId.Equals(id));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _context.UpdateAdmissionRecord(id, toUpdate);
            return Ok(toUpdate);
        }

        [HttpPost("/api/CreateAdmissionRecord")]
        public async Task<ActionResult<AdmissionRecord>> CreateAdmissionRecord([FromBody] AdmissionRecordRegisterDTO _new)
        {
            await _context.CreateAdmissionRecord(_new);

            return CreatedAtAction(nameof(CreateAdmissionRecord), _new.GetHashCode(), _new);
        }
    }
}
