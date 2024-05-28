using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
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

        public void CreateAccount(AccountDTO Account)
        {
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
            _accountService.Create(_account);
        }


        public void DeleteAccount(Account Account)
        {
            throw new NotImplementedException();
        }

        public Account? GetAccountByCondition(Expression<Func<Account, bool>> expression)
        {
            return _accountService.GetByCondition(expression);
        }

        public IEnumerable<Account> GetAccountByRole(string role, string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountService.GetAll();
        }

        public IEnumerable<Account> GetAllAccountsByRole(string role)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount(string id, AccountDTO Account)
        {
            var _account = new Account
            {
                AccountId = id,
                FullName = Account.FullName,
                Password = Account.Password,
                Username = Account.UserName
            };
            _accountService.Update(_account);
        }

        private string GenerateId()
        {
            var prefix = "AC-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;

        }
    }
}
