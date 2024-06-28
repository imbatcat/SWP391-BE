using PetHealthcare.Server.Core.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IMedicalRecordRepository : IRepositoryBase<MedicalRecord>
    {
        Task<IEnumerable<MedicalRecordVetDTO>> GetMedicalRecordsByAppointmentId(string appointmentId);
        bool Any(Expression<Func<MedicalRecord, bool>> predicate);
    }
}
