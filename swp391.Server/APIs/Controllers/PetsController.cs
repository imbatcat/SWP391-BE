﻿using System;
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
        public IEnumerable<Pet> GetPets()
        {
            return _context.GetAllPets();
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
        //{
        //    return await _context.Pets.ToListAsync();
        //}

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet([FromRoute]string id)
        {
            var pet = _context.GetPetByCondition(a=>a.PetId == id);

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
            _context.UpdatePet(id, pet);
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
<<<<<<< Updated upstream
            _context.CreatePet(petDTO);
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
=======
            try
            {
                await _context.CreatePet(petDTO);
                return CreatedAtAction(nameof(PostPet), new { id = petDTO.GetHashCode() }, petDTO);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
           
>>>>>>> Stashed changes
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet([FromRoute]string id)
        {
            var pet = _context.GetPetByCondition(a => a.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.DeletePet(pet);

            return NoContent();
        }
    }
}
