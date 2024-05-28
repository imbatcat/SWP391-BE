using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
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

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Create(Account entity)
        {
            context.Accounts.Add(entity);
            SaveChanges();
        }

        public void Delete(Account entity)
        {
            context.Accounts.Remove(entity);
        }

        public IEnumerable<Account> GetAll()
        {
            return context.Accounts.ToList();
        }

        public Account? GetByCondition(Expression<Func<Account, bool>> expression)
        {
            return context.Accounts.FirstOrDefault(expression);
        }

        public Account? GetAccountById(string id)
        {
            return context.Accounts.FirstOrDefault(a => a.AccountId == id);
        }
        public void Update(Account entity)
        {
            var account = GetByCondition(e => e.AccountId == entity.AccountId);
            if (account != null)
            {
                context.Entry(account).State = EntityState.Modified;
                account.FullName = entity.FullName;
                account.Username = entity.Username;
                account.Password = entity.Password;
                SaveChanges();
            }
        }

        public IEnumerable<Account> GetAccountsByRole(string role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAccountByRole(string role, string id)
        {
            throw new NotImplementedException();
        }
    }
}
