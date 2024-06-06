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
        private readonly IPetRepository _petService;
        private readonly IAdmissionRecordRepository _admissionRecordService;

        public PetService(IPetRepository petService, IAdmissionRecordRepository admissionRecordService)
        {
            _petService = petService;
            _admissionRecordService = admissionRecordService;
        }

        public async Task CreatePet(PetDTO pet)
        {
            var res = await ConfirmPetIdentity(pet.AccountId, pet);
            // if false then throw new exception
            var _pet = new Pet
            {
                PetId = GenerateID(),
                ImgUrl = pet.ImgUrl,
                PetName = pet.PetName,
                PetBreed = pet.PetBreed,
                PetAge = pet.PetAge,
                Description = pet.Description,
                IsMale = pet.IsMale,
                IsCat = pet.IsCat,
                VaccinationHistory = pet.VaccinationHistory,
                IsDisabled = pet.IsDisable,
                AccountId = pet.AccountId
            };
            var checkPet = await _petService.petExist(_pet);
            if (!checkPet)
            {
                await _petService.Create(_pet);
            }
            else
            {
                throw new BadHttpRequestException("An exited pet already had these values.");
            }
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
            return null;
            //return await _petService.GetAccountPets(id);
        }

        public async Task<Pet?> GetPetByCondition(Expression<Func<Pet, bool>> expression)
        {
            return await _petService.GetByCondition(expression);
        }
        public async Task<AdmissionRecord?> GetPetByName(Expression<Func<Pet, bool>> expression)
        {
            var _admissionRecords = await _admissionRecordService.GetAll();
            var born = await _petService.GetByCondition(expression);  //----Long
            AdmissionRecord save;
            foreach (var item in _admissionRecords)
            {

                if (item.PetId.Equals(born.PetId))
                {
                    return save = item;
                }
            }
            return null;
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
            var checkPet = await _petService.petExist(_pet);
            if (!checkPet)
            {
                await _petService.Update(_pet);
            }
            else
            {
                throw new BadHttpRequestException("An exited pet already had these values.");
            }

        }
        public string GenerateID()
        {
            var prefix = "PE-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;
        }
        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPet(string petId)
        {
            return await _petService.GetMedicalRecordsByPet(petId);
        }

        public Task<bool> ConfirmPetIdentity(string AccountId, PetDTO newPet)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AdmissionRecord>> GetAdmissionRecordsByPet(string petId)
        {
            return await _petService.GetAdmissionRecordsByPet(petId);
        }
    }

}
