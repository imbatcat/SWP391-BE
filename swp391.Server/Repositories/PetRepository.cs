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
            await  SaveChanges();
        }

        public void Delete(Pet entity)
        {
            context.Pets.Remove(entity);
        }

        public async Task< IEnumerable<Pet>> GetAll()
        {
            return await context.Pets.ToListAsync();
        }

        public async Task< Pet?> GetByCondition(Expression<Func<Pet, bool>> expression)
        {
            return await context.Pets.FirstOrDefaultAsync(expression);
        }


        public async Task Update(Pet entity)
        {
            var pet =await GetByCondition(e => e.PetId == entity.PetId);
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

        public IEnumerable<Pet> GetAccountPets(string id)
        {
            return context.Pets.Where(p => p.AccountId == id).ToList();
        }
    }
}
