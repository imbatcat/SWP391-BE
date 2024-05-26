using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class CageRepository : ICageRepository
    {
        private readonly PetHealthcareDbContext context;
        public CageRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }
        public void Create(Cage entity)
        {
            context.Cages.Add(entity);
            SaveChanges();
        }

        public void Delete(Cage entity)
        {
            context.Cages.Remove(entity);
        }

        public IEnumerable<Cage> GetAll()
        {
            return context.Cages.ToList();
        }

        public Cage? GetByCondition(Expression<Func<Cage, bool>> expression)
        {
            return context.Cages.FirstOrDefault(expression);
        }

        public Cage? GetCageById(int id)
        {
            return context.Cages.FirstOrDefault(a => a.CageId == id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Cage entity)
        {
            var cage = GetByCondition(e => e == entity);
            if (cage != null)
            {
                context.Entry(entity).State = EntityState.Modified;
                cage.IsOccupied = entity.IsOccupied;

                SaveChanges();
            }
        }
    }
}
