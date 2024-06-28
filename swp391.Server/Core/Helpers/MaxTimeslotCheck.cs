using PetHealthcare.Server.Core.Constant;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.Core.Helpers
{
    public static class MaxTimeslotCheck
    {
        public static async Task<bool> isMaxTimeslotReached(IAppointmentService _appointmentService,string vetId, DateOnly appDate, int timeslotId, bool isCreate)
        {
            IEnumerable<Appointment> appointmentList = await _appointmentService.GetAll();
           
            int checker = 0;
            foreach (Appointment app in appointmentList)
            {
                if (app.VeterinarianAccountId.Equals(vetId)
                    && app.AppointmentDate.Equals(appDate)
                    && app.TimeSlotId == timeslotId)
                {
                    checker++;
                }
            }
            /*isCreate is to check whether the appointment is belong to create or update api, because the difference is create api cannot
            create if timeslot reached maximum but update can*/
            if (checker < ProjectConstant.MAX_APP_PER_TIMESLOT)
            {
                return false;
            }
            else if (!isCreate && checker <= ProjectConstant.MAX_APP_PER_TIMESLOT)
            {
                return false;
            }
            return true;
        }
    }
}
