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

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Create(T entity)
        {
            //var entityType = context.Model.FindEntityType(typeof(T));
            //var tableName = entityType.GetTableName();

            context.Set<T>().Add(entity);
            SaveChanges();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            var list = context.Set<T>();
            return list.ToList();
        }

        public T? GetByCondition(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().FirstOrDefault(expression);
        }

        public void Update(T entity)
        {
            var T = GetByCondition(e => e == entity);
            if (T != null)
            {

                SaveChanges();
            }
        }
    }
}
