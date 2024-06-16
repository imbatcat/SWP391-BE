using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Models.ApplicationModels;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountService;

        public AccountService(IAccountRepository service)
        {
            _accountService = service;
        }

        public async Task<Account?> CreateAccount(AccountDTO Account, bool isGoogle)
        {

            var _account = new Account
            {
                AccountId = GenerateId(),
                Username = Account.UserName,
                FullName = Account.FullName,
                Password = Account.Password,
                DateOfBirth = Account.DateOfBirth != null ? (DateOnly)Account.DateOfBirth : null,
                Email = Account.Email,
                PhoneNumber = Account.PhoneNumber,
                RoleId = Account.RoleId,
                IsMale = Account.IsMale,
                JoinDate = DateOnly.FromDateTime(DateTime.Now),
                IsDisabled = isGoogle ? false : true,
            };

            try
            {
                await _accountService.Create(_account);
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(
                    ex.Message,
                    ex.StatusCode,
                    ex.InnerException);
            }
            return _account;
        }

        public async Task DeleteAccount(Account Account)
        {
            var _account = new Account
            {
                AccountId = Account.AccountId,
                IsDisabled = true
            };
            await _accountService.DeleteAccount(_account);
        }

        public async Task<Account?> GetAccountByCondition(Expression<Func<Account, bool>> expression)
        {
            return await _accountService.GetByCondition(expression);
        }

        public async Task<Account?> GetAccountByRole(int roleId, string id)
        {
            var accounts = await GetAccountByCondition(a => a.RoleId == roleId && a.AccountId.Equals(id));
            if (accounts == null)
            {
                return null;
            }
            return accounts;
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _accountService.GetAll();
        }

        public async Task<IEnumerable<Account>> GetAllAccountsByRole(int roleId)
        {
            return await _accountService.GetAccountsByRole(roleId);
        }

        public async Task UpdateAccount(string id, AccountDTO Account)
        {
            var _account = new Account
            {
                AccountId = id,
                FullName = Account.FullName,
                Password = Account.Password,
                Username = Account.UserName
            };
            await _accountService.Update(_account);
        }

        public string GenerateId()
        {
            var prefix = "AC-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;
        }

        public async Task<bool> SetAccountIsDisabled(RequestAccountDisable account)
        {
            return await _accountService.SetAccountIsDisabled(account);
        }

        public async Task<bool> Any(Expression<Func<Account, bool>> expression)
        {
            return await _accountService.Any(expression);
        }
    }
}
