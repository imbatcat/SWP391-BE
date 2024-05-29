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
        public async Task Create(Cage entity)
        {
            await context.Cages.AddAsync(entity);
            await SaveChanges();
        }

        public void Delete(Cage entity)
        {
            context.Cages.Remove(entity);
        }

        public async Task<IEnumerable<Cage>> GetAll()
        {
            return await context.Cages.ToListAsync();
        }

        public async Task<Cage?> GetByCondition(Expression<Func<Cage, bool>> expression)
        {
            return await context.Cages.FirstOrDefaultAsync(expression);
        }

        public async Task<Cage?> GetCageByID(int Id)
        {
            return await context.Cages.FirstOrDefaultAsync(a => a.CageId == Id);
        }


        public async Task<IEnumerable<Cage>> GetCages(int Id)
        {
            return await context.Cages.OrderBy(p => p.CageId).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(Cage entity)
        {
            var cage = await GetByCondition(e => e.CageId == entity.CageId);
            if (cage != null)
            {
                context.Entry(cage).State = EntityState.Modified;
                cage.IsOccupied = entity.IsOccupied;

                await SaveChanges();
            }
        }

        Task IRepositoryBase<Cage>.Create(Cage entity)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Cage>> IRepositoryBase<Cage>.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<Cage?> IRepositoryBase<Cage>.GetByCondition(Expression<Func<Cage, bool>> expression)
        {
            throw new NotImplementedException();
        }

        Task IRepositoryBase<Cage>.SaveChanges()
        {
            throw new NotImplementedException();
        }

        Task IRepositoryBase<Cage>.Update(Cage entity)
        {
            throw new NotImplementedException();
        }
    }
}
