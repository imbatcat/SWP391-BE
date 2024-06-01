using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IGenericService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetByCondition(Expression<Func<T, bool>> expression);
        Task CreateEntity(T Entity);
        Task UpdateEntity(T Entity);
        void DeleteEntity(T Entity);
    }
}
