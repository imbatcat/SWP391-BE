﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PetHealthcareSystem._3._Services;
using PetHealthcareSystem.DTOS;
using PetHealthcareSystem.Models;

namespace PetHealthcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _context;

        public AccountsController(IAccountService context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
        public IEnumerable<Account> GetAccount()
        {
            return _context.GetAllAccounts();
        }

<<<<<<< Updated upstream:APIs/Controllers/AccountsController.cs
=======
        [HttpGet("/byRole/{role}")]
        public IEnumerable<Account> GetAllAccountsByRole([FromBody] string role)
        {
            return null;
        }

        [HttpGet("/byRole/{role}&{id}")]
        public IEnumerable<Account> GetAccountByRole([FromRoute] string role, [FromRoute] string id)
        {
            return null; 
        }

>>>>>>> Stashed changes:swp391.Server/APIs/Controllers/AccountsController.cs
        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount([FromRoute] string id)
        {
            var account = _context.GetAccountByCondition(a => a.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(string id, AccountDTO account)
        {
            _context.UpdateAccount(account);

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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount([FromBody] AccountDTO accountDTO)
        {
            _context.CreateAccount(accountDTO);
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (AccountExists(account.AccountId))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return CreatedAtAction("GetAccount", new { id = accountDTO.GetHashCode() }, accountDTO);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var account = _context.GetAccountByCondition(a => a.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            _context.DeleteAccount(account);

            return NoContent();
        }
    }
}