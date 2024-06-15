using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetHealthcareDbContext context;
        public PetRepository(PetHealthcareDbContext context)
        {
            this.context = context;
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

        public async Task Delete(Pet entity)
        {
            var pet = await GetByCondition(e => e.PetId == entity.PetId);
            if (pet != null)
            {
                context.Entry(pet).State = EntityState.Modified;
                pet.IsDisabled = entity.IsDisabled;
                await SaveChanges();
            }
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
            return context.MedicalRecords.Any(m => m.PetId == medRecId);
        }
        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPet(string petId)
        {
            if (!CheckMedicalRecord(petId))
            {
                return null;
            }
            var list = await context.MedicalRecords.ToListAsync();
            List<MedicalRecord> medicalRecords = new List<MedicalRecord>();
            foreach (MedicalRecord medRec in list)
            {
                if (medRec.PetId == petId)
                {
                    medicalRecords.Add(medRec);
                }
            }
            return medicalRecords;
        }
        public bool CheckAdmissionRecord(string admissionRecId)
        {
            return context.AdmissionRecords.Any(a => a.PetId == admissionRecId);
        }
        public async Task<IEnumerable<AdmissionRecord>> GetAdmissionRecordsByPet(string petId)
        {
            if (!CheckAdmissionRecord(petId))
            {
                return null;
            }
            var list = await context.AdmissionRecords.ToListAsync();
            List<AdmissionRecord> admissionRecords = new List<AdmissionRecord>();
            foreach (AdmissionRecord admRec in list)
            {
                if (admRec.PetId == petId)
                {
                    admissionRecords.Add(admRec);
                }
            }
            return admissionRecords;
        }


        void IRepositoryBase<Pet>.Delete(Pet entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Pet> GetPetInfoAppointment(string appointmentId)
        {
            var appointment = await context.Appointments.SingleOrDefaultAsync(app => app.AppointmentId == appointmentId);
            return await context.Pets.SingleOrDefaultAsync(pet => pet.PetId == appointment.PetId);
        }

        public async Task<IEnumerable<Pet>> GetAccountPets(string id)
        {
            return await context.Pets.Where(pet => pet.AccountId == id).ToListAsync();
        }
    }
}
