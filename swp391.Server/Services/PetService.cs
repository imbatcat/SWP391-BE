using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class PetService : IPetService
    {
        private readonly IPetService _context;
        private readonly IPetRepository _petService;
        public PetService(IPetRepository petService)
        {
            _petService = petService;
        }
        public void CreatePet(PetDTO pet)
        {
            var _pet = new Pet
            {
                PetId = GenerateID(),
                ImgUrl =pet.ImgUrl,
                PetName = pet.PetName,
                PetBreed = pet.PetBreed,
                PetAge = pet.PetAge,
                Description = pet.Description,
                IsMale = pet.IsMale,
                IsCat = pet.IsCat,
                VaccinationHistory = pet.VaccinationHistory,
                IsDisabled=pet.IsDisable,
                AccountId=pet.AccountId
            };
            _petService.Create(_pet);
        }

        public void DeletePet(Pet pet)
        {
            _petService.Delete(pet);
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _petService.GetAll();
        }

        public Pet? GetPetByCondition(Expression<Func<Pet, bool>> expression)
        {
            return _petService.GetByCondition(expression);
        }

        public void UpdatePet(string id, PetDTO pet)
        {
            var _pet = new Pet
            {
                PetName = pet.PetName,
                PetAge = pet.PetAge,
                Description = pet.Description,
                VaccinationHistory = pet.VaccinationHistory,
                IsDisabled = pet.IsDisable
            };
            _petService.Update(_pet);
        }
        private string GenerateID()
        {
            var prefix = "PE-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;
        }
    }
    
}
