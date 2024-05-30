using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountService;

        public AccountService(IAccountRepository accountService)
        {
            _accountService = accountService;
        }

        public async Task CreateAccount(AccountDTO Account)
        {
            var emailAuth = new EmailAddressAttribute();
            if (!emailAuth.IsValid(Account.Email)) throw new BadHttpRequestException("Invalid email");
            
            var _account = new Account
            {
                AccountId = GenerateId(),
                Username = Account.UserName,
                FullName = Account.FullName,
                Password = Account.Password,
                DateOfBirth = Account.DateOfBirth,
                Email = Account.Email,
                PhoneNumber = Account.PhoneNumber,
                IsMale = Account.IsMale,
                JoinDate = new DateOnly()
            };
            await _accountService.Create(_account);
        }

        public void DeleteAccount(Account Account)
        {
            throw new NotImplementedException();
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
    }
}
