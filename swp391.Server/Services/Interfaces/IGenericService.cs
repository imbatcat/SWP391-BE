using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IGenericService<T>
    {
        IEnumerable<T> GetAll();
        T? GetByCondition(Expression<Func<T, bool>> expression);
        void CreateAccount(T Entity);
        void UpdateEntity(T Entity);
        void DeleteEntity(T Entity);
    }
}
