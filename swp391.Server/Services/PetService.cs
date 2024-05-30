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
        public async Task CreatePet(PetDTO pet)
        {
            var res = await ConfirmPetIdentity(pet.AccountId, pet);
            // if false then throw new exception
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
            await _petService.Create(_pet);
        }

        public void DeletePet(Pet pet)
        {
            _petService.Delete(pet);
        }
        public async Task<IEnumerable<Pet>> GetAllPets()
        {
            return await _petService.GetAll();
        }

        public async Task<IEnumerable<Pet>> GetAccountPets(string id)
        {
            return await _petService.GetAccountPets(id);
        }

        public async Task< Pet?> GetPetByCondition(Expression<Func<Pet, bool>> expression)
        {
            return await _petService.GetByCondition(expression);
        }

        public async Task UpdatePet(string id, PetDTO pet)
        {
            var _pet = new Pet
            {
                PetId = id,
                PetName = pet.PetName,
                Description = pet.Description,
                VaccinationHistory = pet.VaccinationHistory,
                IsDisabled = pet.IsDisable
            };
            await _petService.Update(_pet);
        }
        public string GenerateID()
        {
            var prefix = "PE-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;
        }

        public async Task<bool> ConfirmPetIdentity(string AccountId, PetDTO newPet)
        {
            // newPet's name, breed and isCat must not match any pets of this owner in the database

            //get list of pet by accound id, then check if theres any pet in the database matches the 
            //mentioned props of newPets, if yes then return false, true if otherwise. 
            return false;
        }
    }
    
}
