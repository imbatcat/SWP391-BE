using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IPetService
    {
        IEnumerable<Pet> GetAllPets();
        Pet? GetPetByCondition(Expression<Func<Pet, bool>> expression);
        void CreatePet(PetDTO pet);
        void UpdatePet(string id,PetDTO pet);
        void DeletePet(Pet pet);
    }
}
