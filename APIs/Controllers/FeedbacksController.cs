using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetHealthcareSystem._3._Services;
using PetHealthcareSystem.APIs.DTOS;
using PetHealthcareSystem.Models;

namespace PetHealthcareSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _context;

        public FeedbacksController(IFeedbackService context)
        {
            _context = context;
        }

        // GET: api/Feedbacks
        [HttpGet]
        public  IEnumerable<Feedback> GetFeedbacks()
        {
            return  _context.GetAllFeedback();
        }

        //// GET: api/Feedbacks/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Feedback>> GetFeedback(int id)
        //{
        //    var feedback = await _context.Feedbacks.FindAsync(id);

        //    if (feedback == null)
        //    {
        //        return NotFound();
        //    }

        //    return feedback;
        //}

        // PUT: api/Feedbacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // POST: api/Feedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback([FromBody] FeedbackDTO feedback)
        {
           
            return CreatedAtAction("GetFeedback", new { id = feedback.GetHashCode() }, feedback);
        }

        // DELETE: api/Feedbacks/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteFeedback(int id)
        //{
        //    var feedback = await _context.Feedbacks.FindAsync(id);
        //    if (feedback == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Feedbacks.Remove(feedback);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
