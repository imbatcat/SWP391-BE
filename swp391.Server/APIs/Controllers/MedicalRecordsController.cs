using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService _context;

        public MedicalRecordsController(IMedicalRecordService context)
        {
            _context = context;
        }

        // GET: api/MedicalRecords
        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MedicalRecord>))]
        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecords()
        {
            return await _context.GetAllMedicalRecord();
        }

        // GET: api/MedicalRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(string id)
        {
            var medicalRecord = await _context.GetMedicalRecordByCondition(m=>m.MedicalRecordId == id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return medicalRecord;
        }

        //PUT: api/MedicalRecords/5
         //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalRecord(string id, MedicalRecordDTO medicalRecord)
        {
            await _context.UpdateMedicalRecord(id, medicalRecord);
            return NoContent();
        }

        // POST: api/MedicalRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> PostMedicalRecord([FromBody] MedicalRecordDTO medicalRecordDTO)
        {
            _context.CreateMedicalRecord(medicalRecordDTO);
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (MedicalRecordExists(medicalRecord.MedicalRecordId))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return CreatedAtAction(nameof(PostMedicalRecord), new { id = medicalRecordDTO.MedicalRecordId }, medicalRecordDTO);
        }

        //// DELETE: api/MedicalRecords/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMedicalRecord(string id)
        //{
        //    var medicalRecord = await _context.MedicalRecords.FindAsync(id);
        //    if (medicalRecord == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.MedicalRecords.Remove(medicalRecord);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool MedicalRecordExists(string id)
        //{
        //    return _context.MedicalRecords.Any(e => e.MedicalRecordId == id);
        //}
    }
}
