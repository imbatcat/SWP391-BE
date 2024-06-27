using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Core.DTOS;
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
                dickHead.DischargeDate = entity.DischargeDate;
                dickHead.IsDischarged = entity.IsDischarged;
                await SaveChanges();
            }
        }

        public async Task DischargePet(string petId)
        {
            var admissionRecord = await GetByCondition(ad => ad.PetId == petId);
            if (admissionRecord != null)
            {
                if (admissionRecord.IsDischarged == false)
                {
                    _context.Entry(admissionRecord).State = EntityState.Modified;
                    admissionRecord.IsDischarged = true;
                    admissionRecord.PetCurrentCondition = "Is discharged";
                    admissionRecord.DischargeDate = DateOnly.FromDateTime(DateTime.Now);
                    await SaveChanges();
                }
            }
        }

        public async Task UpdateCondition(string petId, UpdatePetConditionDTO updatePetConditionDTO)
        {
            var admissionRecord = await GetByCondition(ad => ad.PetId == petId);
            if (admissionRecord != null)
            {
                if (admissionRecord.IsDischarged == false)
                {
                    _context.Entry(admissionRecord).State = EntityState.Modified;
                    admissionRecord.PetCurrentCondition = updatePetConditionDTO.PetCurrentCondition;
                    await SaveChanges();
                }
            }
        }
    }
}
