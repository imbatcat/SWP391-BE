using PetHealthcare.Server.Core.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleService;
        public RoleService(IRoleRepository roleService)
        {
            _roleService = roleService;
        }
        public async Task CreateRole(RoleDTO Role)
        {
            Role toCreateRole = new Role
            {
                RoleName = Role.RoleName
            };
            await _roleService.Create(toCreateRole);
        }

        public void DeleteRole(Role role)
        {
            _roleService.Delete(role);
        }

        public async Task<IEnumerable<Role>> GetAllRole()
        {
            return await _roleService.GetAll();
        }

        public async Task<Role?> GetRoleByCondition(Expression<Func<Role, bool>> expression)
        {
            return await _roleService.GetByCondition(expression);
        }

        public async Task UpdateRole(int id, RoleDTO role)
        {
            Role UpdateRole = new Role
            {
                RoleId = id,
                RoleName = role.RoleName
            };
            await _roleService.Update(UpdateRole);
        }
    }
}
