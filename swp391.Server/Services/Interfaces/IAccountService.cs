using Microsoft.AspNetCore.Identity;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<IEnumerable<Account>> GetAllAccountsByRole(int roleId);
        Task<Account?> GetAccountByRole(int roleId, string id);
        Task<Account?> GetAccountByCondition(Expression<Func<Account, bool>> expression);
        Task<Account?> CreateAccount(AccountDTO Account, bool isGoogle);
        Task UpdateAccount(string id, AccountDTO Account);
        Task DeleteAccount(Account Account);
        Task<bool> SetAccountIsDisabled(RequestAccountDisable account);
        Task<bool> Any(Expression<Func<Account, bool>> expression);

        string GenerateId();
    }

}
