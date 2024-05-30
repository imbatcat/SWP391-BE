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

        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public void Create(Pet entity)
        {
<<<<<<< Updated upstream
            context.Pets.Add(entity);
            SaveChanges();
=======
            if(petExit(entity)) 
            {
                throw new InvalidOperationException("A pet with the same data has already exits");
            }
            await context.Pets.AddAsync(entity);
            await  SaveChanges();
>>>>>>> Stashed changes
        }

        public void Delete(Pet entity)
        {
            context.Pets.Remove(entity);
        }

        public IEnumerable<Pet> GetAll()
        {
            return context.Pets.ToList();
        }

        public Pet? GetByCondition(Expression<Func<Pet, bool>> expression)
        {
            return context.Pets.FirstOrDefault(expression);
        }


        public void Update(Pet entity)
        {
            var pet = GetByCondition(e => e.PetId == entity.PetId);
            if (pet != null)
            {
                context.Entry(pet).State = EntityState.Modified;
                pet.PetName = entity.PetName;
                pet.Description = entity.Description;
                pet.PetAge = entity.PetAge;
                pet.VaccinationHistory = entity.VaccinationHistory;
                SaveChanges();
            }
        }
        public Pet? GetPetById(string id)
        {
            return context.Pets.FirstOrDefault(a => a.PetId == id);
        }
        public bool petExit(Pet pet)
        {
            return context.Pets.Contains(pet);
        }
    }
}
