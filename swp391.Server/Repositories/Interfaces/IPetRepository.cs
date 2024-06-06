using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IPetRepository : IRepositoryBase<Pet>
    {
        public Task<bool> petExist(Pet pet);
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPet(string petId);
        Task<IEnumerable<AdmissionRecord>> GetAdmissionRecordsByPet(string petId);
    }
}
