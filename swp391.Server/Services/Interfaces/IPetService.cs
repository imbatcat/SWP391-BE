using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IPetService
    {
        Task< IEnumerable<Pet>> GetAllPets();
        Task< Pet?> GetPetByCondition(Expression<Func<Pet, bool>> expression);
        Task CreatePet(PetDTO pet);
        Task UpdatePet(string id,PetDTO pet);
        void DeletePet(Pet pet);
        Task<IEnumerable<Pet>> GetAccountPets(string id);
        string GenerateID();

        Task<bool> ConfirmPetIdentity(string AccountId, PetDTO newPet);
    }
}
