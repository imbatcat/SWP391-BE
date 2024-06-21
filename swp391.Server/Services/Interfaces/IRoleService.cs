using PetHealthcare.Server.Core.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRole();
        Task<Role?> GetRoleByCondition(Expression<Func<Role, bool>> expression);
        Task CreateRole(RoleDTO Role);
        Task UpdateRole(int id, RoleDTO Role);
        void DeleteRole(Role role);
    }
}
