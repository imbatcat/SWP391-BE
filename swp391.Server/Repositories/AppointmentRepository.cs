using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly PetHealthcareDbContext context;
        public AppointmentRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }
        public async Task Create(Appointment entity)
        {
            await context.Appointments.AddAsync(entity);
            await SaveChanges();
        }
        public bool isInputtedVetIdValid (string id) 
        {
            if(context.Accounts.Find(id) != null)
            {
                if(id.StartsWith('V')) 
                { return true; }
            }
            return false;
        }
        public void Delete(Appointment entity)
        {
            context.Appointments.Remove(entity);
            context.SaveChanges();
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await context.Appointments.Include(a => a.Pet).Include(a => a.Veterinarian).Include(a => a.TimeSlot).ToListAsync();
        }

        public async Task<Appointment?> GetByCondition(Expression<Func<Appointment, bool>> expression)
        {
            return await context.Appointments.FirstOrDefaultAsync(expression);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(Appointment entity)
        {
            var appointment = await GetByCondition(a => a.AppointmentId == entity.AppointmentId);
            if (appointment != null)
            {
                // this line ensures efcore to update the table.
                context.Entry(appointment).State = EntityState.Modified;

                appointment.AppointmentDate = entity.AppointmentDate;
                appointment.AppointmentNotes = entity.AppointmentNotes;
                appointment.TimeSlotId = entity.TimeSlotId;
                appointment.VeterinarianAccountId = entity.VeterinarianAccountId;
                await SaveChanges();
            }
        }

        public async Task<Account?> GetAccountById(string id)
        {
            return await context.Accounts.FindAsync(id);
        }
    }
}
