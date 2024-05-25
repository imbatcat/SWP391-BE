using Microsoft.Identity.Client;
using NanoidDotNet;
using PetHealthcareSystem._2._Repositories;
using PetHealthcareSystem.DTOS;
using PetHealthcareSystem.Models;
using System.Linq.Expressions;

namespace PetHealthcareSystem._3._Services
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

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountService.GetAll();
        }

        public void UpdateAccount(AccountDTO Account)
        {
            var _account = new Account
            {
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
