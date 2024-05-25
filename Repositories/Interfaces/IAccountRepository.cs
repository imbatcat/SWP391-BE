using PetHealthcareSystem.Models;

namespace PetHealthcareSystem._2._Repositories
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Account? GetAccountById(string id); 
    }
}
