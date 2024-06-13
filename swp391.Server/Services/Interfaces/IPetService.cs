using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.AppointmentDTOs;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> GetAllPets();
        Task<Pet?> GetPetByCondition(Expression<Func<Pet, bool>> expression);
        Task CreatePet(PetDTO pet);
        Task UpdatePet(string id, PetDTO pet);
        Task DeletePet(Pet pet);
        Task<IEnumerable<Pet>> GetAccountPets(string id);
        string GenerateID();

        Task<PetInfoAppointmentDTO> GetPetInfoAppointment(string appointmentId);
        Task<bool> ConfirmPetIdentity(string AccountId, PetDTO newPet);
        Task<IEnumerable<AdmissionRecord>> GetAdmissionRecordsByPet(string petId);
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPet(string petId);
        Task<AdmissionRecord?> GetPetByName(Expression<Func<Pet, bool>> expression);
    }
}
