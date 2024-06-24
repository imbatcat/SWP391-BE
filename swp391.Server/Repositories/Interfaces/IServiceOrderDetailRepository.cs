using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IServiceOrderDetailRepository : IRepositoryBase<ServiceOrderDetails>
    {
        Task<IEnumerable<ServiceOrderDetails>> getAllServieOrderDetail();
    }
}
