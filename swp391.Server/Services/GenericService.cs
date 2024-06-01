using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _entityService;

        public GenericService(IGenericRepository<T> entityService)
        {
            _entityService = entityService;
        }

        public async Task CreateEntity(T Entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(T Entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entityService.GetAll();
        }

        public async Task<T?> GetByCondition(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateEntity(T Entity)
        {
            throw new NotImplementedException();
        }
    }
}
