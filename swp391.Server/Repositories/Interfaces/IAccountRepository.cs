using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        IEnumerable<Account> GetAccountsByRole(string role);
        IEnumerable<Account> GetAccountByRole(string role, string id);
    }
}
