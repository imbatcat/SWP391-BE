using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class TimeslotRepository : ITimeslotRepository
    {
        public void Create(TimeSlot entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TimeSlot entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeSlot> GetAll()
        {
            throw new NotImplementedException();
        }

        public TimeSlot? GetByCondition(Expression<Func<TimeSlot, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(TimeSlot entity)
        {
            throw new NotImplementedException();
        }
    }
}
