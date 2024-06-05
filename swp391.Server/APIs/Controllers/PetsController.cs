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
    public class PetsController : ControllerBase
    {
        private readonly IPetService _context;

        public PetsController(IPetService context)
        {
            _context = context;
        }

        // GET: api/Pets
        [HttpGet("")]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Pet>))]
        public async Task <IEnumerable<Pet>> GetPets()
        {
            return await _context.GetAllPets();
        }
        [HttpGet("/api/medRecByPet/{petId}")]
        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPet([FromRoute]string petId)
        {
            return await _context.GetMedicalRecordsByPet(petId);
        }

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet([FromRoute]string id)
        {
            var pet = await _context.GetPetByCondition(a=>a.PetId == id);

            if (pet == null)
            {
                return NotFound();
            }
            return pet;
        }

        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(string id, PetDTO pet)
        {
            await _context.UpdatePet(id, pet);
            //if (id != pet.PetId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(pet).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PetExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet([FromBody] PetDTO petDTO)
        {
            try
            {
                await _context.CreatePet(petDTO);
                return CreatedAtAction(nameof(PostPet), new { id = petDTO.GetHashCode() }, petDTO);
            }catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet([FromRoute]string id)
        {
            var pet = await _context.GetPetByCondition(a => a.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.DeletePet(pet);

            return NoContent();
        }
    }
}
