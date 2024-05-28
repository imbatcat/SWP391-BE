using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class Timeslot : ITimeSlotService
    {
        private readonly ITimeslotRepository _timeSlotService;

        public Timeslot(ITimeslotRepository timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }
        public void CreateTimeSlot(TimeslotDTO timeSlot)
        {
            var _timeSlot = new TimeSlot
            {
                TimeSlotId = GenerateId(),
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime,
                
            };
            _timeSlotService.Create(_timeSlot);
        }

        public void DeleteTimeSlot(TimeSlot TimeSlot)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeSlot> GetAllTimeSlots()
        {
            return _timeSlotService.GetAll();
        }

        public TimeSlot? GetTimeSlotByCondition(Expression<Func<TimeSlot, bool>> expression)
        {
            return _timeSlotService.GetByCondition(expression);
        }

        public void UpdateTimeSlot(int id, TimeslotDTO TimeSlot)
        {
            var _timeSlot = new TimeSlot
            {
                TimeSlotId = id,
                StartTime = TimeSlot.StartTime,
                EndTime = TimeSlot.EndTime,
            };
            _timeSlotService.Update(_timeSlot);
        }
        private int GenerateId()
        {
            string id = Nanoid.Generate(size: 8);
            return id.GetHashCode();
        }
    }
}
