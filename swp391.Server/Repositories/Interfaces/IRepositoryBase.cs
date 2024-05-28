using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> GetAll();
        T? GetByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
