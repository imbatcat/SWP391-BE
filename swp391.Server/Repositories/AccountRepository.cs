using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Collections;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PetHealthcareDbContext context;

        public AccountRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task Create(Account entity)
        {
            await context.Accounts.AddAsync(entity);
            await SaveChanges();
        }

        public void Delete(Account entity)
        {
            context.Accounts.Remove(entity);
            SaveChanges();
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await context.Accounts.ToListAsync();
        }

        public async Task<Account?> GetByCondition(Expression<Func<Account, bool>> expression)
        {
            return await context.Accounts.FirstOrDefaultAsync(expression);
        }

        public async Task Update(Account entity)
        {
            var account = await GetByCondition(e => e.AccountId == entity.AccountId);
            if (account != null)
            {
                context.Entry(account).State = EntityState.Modified;
                account.FullName = entity.FullName;
                account.Username = entity.Username;
                account.Password = entity.Password;
                await SaveChanges();
            }
        }
        public bool CheckRoleId(int roleId)
        {
           return context.Roles.Any(r => r.RoleId == roleId);
        }
        public async Task<IEnumerable<Account>> GetAccountsByRole(int roleId)
        {
            if(!CheckRoleId(roleId))
            {
                return null;
            }
            var list = await GetAll();
            List<Account> accounts = new List<Account>();
            foreach (Account acc in list)
            {
                if (acc.RoleId == roleId)
                {
                    accounts.Add(acc);
                }
            }
            return accounts;
        }

        public async Task<Account?> GetAccountByRole(int roleId, string id)
        {
            var accounts = await GetByCondition(a => a.RoleId == roleId && a.AccountId.Equals(id));
            if(accounts == null)
            {
                return null;
            }
            return accounts;
        }

        public Task<Account?> LoginAccount(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
