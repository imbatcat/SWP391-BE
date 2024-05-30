using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface ICageRepository : IRepositoryBase<Cage>
    {
        Task<IEnumerable<Cage>> GetCages(int Id);
        Task<Cage?> GetCageByID (int Id);
    }
}
