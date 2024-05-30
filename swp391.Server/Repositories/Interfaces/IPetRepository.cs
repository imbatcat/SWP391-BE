using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IPetRepository :IRepositoryBase<Pet>
    {
        IEnumerable<Pet> GetAccountPets(string id);
    }
}
