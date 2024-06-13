using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, Customer")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _context;
        private readonly IPetService _contextPet;

        public AccountsController(IAccountService context, IPetService contextPet)
        {
            _context = context;
            _contextPet = contextPet;
        }

        // GET: api/Accounts
        //<summary>
        //get all of the account
        //</summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _context.GetAllAccounts();
        }

        //get all account with the same role
        [HttpGet("/api/byRole/{roleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<Account>> GetAllAccountsByRole([FromRoute] int roleId)
        {
            return await _context.GetAllAccountsByRole(roleId);
        }
        //get a single account with specific role and id
        [HttpGet("/api/byRole/{roleId}&{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Account>> GetAccountByRole([FromRoute] string roleId, [FromRoute] string id)
        {
            var checkAccount = await _context.GetAccountByCondition(a => a.AccountId == id);
            if (checkAccount == null)
            {
                return NotFound();
            }
            return Ok(checkAccount);
        }
        //get the list of pet of this account has the input id
        [HttpGet("/api/accounts/pets/{accountId}")]
        public async Task<IEnumerable<Pet>> GetAccountPets([FromRoute] string accountId)
        {
            return await _contextPet.GetAccountPets(accountId);

        }

        // GET: get the account with the input id
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount([FromRoute] string id)
        {
            var account = await _context.GetAccountByCondition(a => a.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // change the information of the account
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(string id, AccountDTO account)
        {
            await _context.UpdateAccount(id, account);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!AccountExists(id))
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

        // POST: create a new user and insert it into database
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount([FromBody] AccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad input");
            }
            try
            {
                var result = await _context.CreateAccount(accountDTO);
            }
            catch (BadHttpRequestException ex)
            {
                return (BadRequest(ex.Message));
            }
            return CreatedAtAction(
                     "GetAccount", new { id = accountDTO.GetHashCode() }, accountDTO);

        }

        // DELETE: change the status of the account to true, not show it to the customer
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var account = await _context.GetAccountByCondition(a => a.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            await _context.DeleteAccount(account);

            return NoContent();
        }
    }
}
