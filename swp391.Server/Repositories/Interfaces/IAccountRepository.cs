using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<IEnumerable<Account>> GetAccountsByRole(int roleId);
        Task<Account?> GetAccountByRole(int roleId, string id);
         
    }
}
