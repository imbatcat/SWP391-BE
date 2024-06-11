using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Customer, Staff, Admin, Vet")]
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pet>))]
        public async Task<IEnumerable<Pet>> GetPets()
        {
            return await _context.GetAllPets();
        }

        //Get all medical Records of a Pet from Pet Id
        [HttpGet("/api/medRecByPet/{petId}")]
        [Authorize(Roles = "Vet, Admin, Customer")]
        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPet([FromRoute]string petId)
        {
            return await _context.GetMedicalRecordsByPet(petId);
        }
        /*Get single pet by a unique PetId*/
        [HttpGet("{id}")]
        [Authorize(Roles="Staff, Customer, Admin")]
        public async Task<ActionResult<Pet>> GetPet([FromRoute] string id)
        {
            var pet = await _context.GetPetByCondition(a => a.PetId == id);

            if (pet == null)
            {
                return NotFound();
            }
            return pet;
        }

        //Get all admission Records of a Pet from Pet Id
        [HttpGet("/api/admRecByPet/{petId}")]
        public async Task<IEnumerable<AdmissionRecord>> GetAdmissionRecordsByPet([FromRoute]string petId)
        {
            return await _context.GetAdmissionRecordsByPet(petId);
        }

        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<IActionResult> PutPet(string id, PetDTO pet)
        {
            await _context.UpdatePet(id, pet);
            return Ok(pet);
        }

        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles="Customer,Staff, Admin")]
        public async Task<ActionResult<Pet>> PostPet([FromBody] PetDTO petDTO)
        {
            await _context.CreatePet(petDTO);
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (PetExists(pet.PetId))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return CreatedAtAction(nameof(PostPet), new { id = petDTO.GetHashCode() }, petDTO);
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Customer,Admin")]
        public async Task<ActionResult<Pet>> DeletePet([FromRoute] string id)
        {
            var pet = await _context.GetPetByCondition(a => a.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            await _context.DeletePet(pet);

            return Ok(pet);
        }
    }
}
