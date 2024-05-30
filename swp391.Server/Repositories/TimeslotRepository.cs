using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class TimeslotRepository : ITimeslotRepository
    {
        private readonly PetHealthcareDbContext _context;

        public TimeslotRepository(PetHealthcareDbContext context)
        {
            _context = context;
        }

        public async Task Create(TimeSlot entity)
        {
            await _context.TimeSlots.AddAsync(entity);
            await SaveChanges();
        }

        public void Delete(TimeSlot entity)
        {
            _context.TimeSlots.Remove(entity);
        }

        public async Task<IEnumerable<TimeSlot>> GetAll()
        {
            return await _context.TimeSlots.OrderBy(p => p.TimeSlotId).ToListAsync();
        }

        public async Task<TimeSlot?> GetByCondition(Expression<Func<TimeSlot, bool>> expression)
        {
            return await _context.TimeSlots.FirstOrDefaultAsync(expression);
        }

        public async Task<TimeSlot?> GetSlotById(int timeId)
        {
            return await _context.TimeSlots.FirstOrDefaultAsync(p => p.TimeSlotId == timeId); 
        }

        public async  Task<IEnumerable<TimeSlot>> GetSlots()
        {
            return await _context.TimeSlots.OrderBy(p => p.TimeSlotId).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(TimeSlot entity)
        {
            var timeSlot = await GetByCondition(e => e.TimeSlotId == entity.TimeSlotId);
            if (timeSlot != null)
            {
                _context.Entry(timeSlot).State = EntityState.Modified;
                timeSlot.TimeSlotId = entity.TimeSlotId;
                timeSlot.StartTime = entity.StartTime;
                timeSlot.EndTime = entity.EndTime;
                await SaveChanges();
            }
        }

      
    }
}
