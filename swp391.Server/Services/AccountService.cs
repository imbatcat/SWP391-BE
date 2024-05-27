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

        public void CreateAccount(AccountDTO Account)
        {
            var attr = new EmailAddressAttribute();
            if (!attr.IsValid(Account.Email)) throw new ArgumentException("Invalid email");

            var _account = new Account
            {
                AccountId = GenerateId(Account.RoleId),
                RoleId = Account.RoleId,
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

        public IEnumerable<Pet> GetAccountPets(Account account)
        {
            return _accountService.GetAccountPets(account);
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

        private string GenerateId(int roleId)
        {
            var prefix = "";
            switch (roleId) {
                case 1:
                    prefix = "AC"; 
                    break;
                case 2:
                    prefix = "AD";
                    break;
                case 3:
                    prefix = "VT";
                    break;
                case 4:
                    prefix = "ST";
                    break;
            }
            prefix += "-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;

        }
    }
}
