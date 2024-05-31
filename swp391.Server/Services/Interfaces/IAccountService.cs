using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IAccountService 
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<IEnumerable<Account>> GetAllAccountsByRole(int roleId);
        Task<Account?> GetAccountByRole(int roleId, string id);
        Task<Account?> GetAccountByCondition(Expression<Func<Account, bool>> expression);
        Task CreateAccount(AccountDTO Account);
        Task UpdateAccount(string id, AccountDTO Account);
        void DeleteAccount(Account Account);
        Task<Account?> LoginAccount();

        string GenerateId();
    }
}
