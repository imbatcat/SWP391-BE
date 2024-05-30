using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class AdmissionRecordRepository : IAdmissionRecordRepository
    {
        private readonly PetHealthcareDbContext _context;

        public AdmissionRecordRepository(PetHealthcareDbContext context)
        {
            _context = context;
        }

        public async Task Create(AdmissionRecord entity)
        {
            await _context.AdmissionRecords.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(AdmissionRecord entity)
        {
            _context.AdmissionRecords.Remove(entity);
        }

        public async Task<IEnumerable<AdmissionRecord>> GetAll()
        {
            return await _context.AdmissionRecords.OrderBy(x => x.AdmissionId).ToListAsync();
        }

        public async Task<AdmissionRecord?> GetByCondition(Expression<Func<AdmissionRecord, bool>> expression)
        {
            return await _context.AdmissionRecords.FirstOrDefaultAsync(expression);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(AdmissionRecord entity)
        {
            var dickHead = await GetByCondition(x => x.AdmissionId == entity.AdmissionId);
            if (dickHead != null)
            {
                _context.Entry(dickHead).State = EntityState.Modified;
                dickHead.PetCurrentCondition = entity.PetCurrentCondition;
                dickHead.PetId = entity.PetId;
                dickHead.CageId = entity.CageId;
                await SaveChanges();
            }
        }
    }
}
