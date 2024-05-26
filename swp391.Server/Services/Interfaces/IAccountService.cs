using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAllAccounts();
        Account? GetAccountByCondition(Expression<Func<Account, bool>> expression);
        void CreateAccount(AccountDTO Account);
        void UpdateAccount(AccountDTO Account);
        void DeleteAccount(Account Account);
    }
}
