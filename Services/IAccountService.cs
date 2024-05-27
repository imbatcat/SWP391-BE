using Microsoft.CodeAnalysis.CSharp.Syntax;
using PetHealthcareSystem.DTOS;
using PetHealthcareSystem.Models;
using System.Linq.Expressions;

namespace PetHealthcareSystem._3._Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAllAccounts();
        Account? GetAccountByCondition(Expression<Func<Account, bool>> expression);
        void CreateAccount(AccountDTO Account);
        void UpdateAccount(string id, AccountDTO Account);
        void DeleteAccount(Account Account);
    }
}
