using PetHealthcareSystem._2._Repositories;
using PetHealthcareSystem.Models;
using System.Linq.Expressions;

namespace PetHealthcareSystem._3._Services
{
    public class GenericService<T> : IGenericService<T> where T : class     
    {
        private readonly IGenericRepository<T> _entityService;

        public GenericService(IGenericRepository<T> entityService)
        {
            _entityService = entityService;
        }

        public void CreateAccount(T Entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(T Entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _entityService.GetAll();
        }

        public T? GetByCondition(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(T Entity)
        {
            throw new NotImplementedException();
        }
    }
}
