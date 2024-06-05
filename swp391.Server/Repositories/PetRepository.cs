using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetHealthcareDbContext context;
        private readonly MedicalRecordRepository medicalRecordRepository;
        public PetRepository(PetHealthcareDbContext context, MedicalRecordRepository medicalRecordRepository)
        {
            this.context = context;
            this.medicalRecordRepository = medicalRecordRepository;
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
        public async Task Create(Pet entity)
        {

            await context.Pets.AddAsync(entity);
            await SaveChanges();
        }

        public void Delete(Pet entity)
        {
            context.Pets.Remove(entity);
        }

        public async Task<IEnumerable<Pet>> GetAll()
        {
            return await context.Pets.ToListAsync();
        }

        public async Task<Pet?> GetByCondition(Expression<Func<Pet, bool>> expression)
        {
            return await context.Pets.FirstOrDefaultAsync(expression);
        }


        public async Task Update(Pet entity)
        {
            var pet = await GetByCondition(e => e.PetId == entity.PetId);
            if (pet != null)
            {
                context.Entry(pet).State = EntityState.Modified;
                pet.PetName = entity.PetName;
                pet.Description = entity.Description;
                pet.PetAge = entity.PetAge;
                pet.VaccinationHistory = entity.VaccinationHistory;
                await SaveChanges();
            }
        }
        public Pet? GetPetById(string id)
        {
            return context.Pets.FirstOrDefault(a => a.PetId == id);
        }
        public async Task<bool> petExist(Pet pet)
        {
            return await context.Pets.AnyAsync(
             p => p.PetName == pet.PetName &&
             p.PetBreed == pet.PetBreed &&
             p.IsMale == pet.IsMale &&
             p.IsCat == pet.IsCat &&
             p.AccountId == pet.AccountId);
        }
        public bool CheckMedicalRecord(string medRecId)
        {
            return context.MedicalRecords.Any(m=>m.PetId == medRecId);
        }
        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPet(string petId)
        {
            if(!CheckMedicalRecord(petId))
            {
                return null;
            }
            var list = await medicalRecordRepository.GetAll();
            List<MedicalRecord> medicalRecords= new List<MedicalRecord>();
            foreach(MedicalRecord medRec in list)
            {
                if(medRec.PetId == petId)
                {
                    medicalRecords.Add(medRec);
                }
            }
            return medicalRecords;
        }
    }
}
