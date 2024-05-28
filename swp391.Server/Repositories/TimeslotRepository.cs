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

        public void Create(TimeSlot entity)
        {
            _context.TimeSlots.Add(entity);
            SaveChanges();
        }

        public void Delete(TimeSlot entity)
        {
            _context.TimeSlots.Remove(entity);
        }

        public IEnumerable<TimeSlot> GetAll()
        {
            return _context.TimeSlots.OrderBy(p => p.TimeSlotId).ToList();
        }

        public TimeSlot? GetByCondition(Expression<Func<TimeSlot, bool>> expression)
        {
            return _context.TimeSlots.FirstOrDefault(expression);
        }

        public TimeSlot? GetSlotById(int timeId)
        {
            return _context.TimeSlots.FirstOrDefault(p => p.TimeSlotId == timeId); 
        }

        public IEnumerable<TimeSlot> GetSlots()
        {
            return _context.TimeSlots.OrderBy(p => p.TimeSlotId).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(TimeSlot entity)
        {
            var timeSlot = GetByCondition(e => e.TimeSlotId == entity.TimeSlotId);
            if (timeSlot != null)
            {
                _context.Entry(timeSlot).State = EntityState.Modified;
                timeSlot.TimeSlotId = entity.TimeSlotId;
                timeSlot.StartTime = entity.StartTime;
                timeSlot.EndTime = entity.EndTime;
                SaveChanges();
            }
        }
    }
}
