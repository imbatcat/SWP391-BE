using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PetHealthcareDbContext context;

        public GenericRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task Create(T entity)
        {
            //var entityType = context.Model.FindEntityType(typeof(T));
            //var tableName = entityType.GetTableName();

            await context.Set<T>().AddAsync(entity);
            await SaveChanges();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var list = context.Set<T>();
            return await list.ToListAsync();
        }

        public async Task<T?> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task Update(T entity)
        {
            var T = await GetByCondition(e => e == entity);
            if (T != null)
            {

                await SaveChanges();
            }
        }
    }
}
