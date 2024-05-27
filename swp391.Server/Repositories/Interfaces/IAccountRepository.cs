using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        IEnumerable<Account> GetAccountsByRole(int roleId);
        Account GetAccountByRole(int roleId, string id);
    }
}
