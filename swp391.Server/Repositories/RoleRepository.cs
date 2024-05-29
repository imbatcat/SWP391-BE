using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PetHealthcareDbContext context;
        public RoleRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }
        public async Task Create(Role entity)
        {
            await context.Roles.AddAsync(entity);
            await SaveChanges();
        }

        public void Delete(Role entity)
        {

            context.Roles.Remove(entity);
            SaveChanges();
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await context.Roles.ToListAsync();
        }

        public async Task<Role?> GetByCondition(Expression<Func<Role, bool>> expression)
        {
            return await context.Roles.FirstOrDefaultAsync(expression);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(Role entity)
        {
            var role = await GetByCondition(e => e.RoleId == entity.RoleId);
 
            if (role != null)
            {
                // this line ensures efcore to update the table.
                context.Entry(role).State = EntityState.Modified;

                role.RoleName = entity.RoleName;
                await SaveChanges();
            }
        }
    }
}
