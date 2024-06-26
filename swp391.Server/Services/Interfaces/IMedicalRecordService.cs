using PetHealthcare.Server.Core.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecord>> GetAllMedicalRecord();
        Task<MedicalRecord?> GetMedicalRecordByCondition(Expression<Func<MedicalRecord, bool>> expression);
        Task CreateMedicalRecord(MedicalRecorResDTO medicalRecord);
        Task UpdateMedicalRecord(string id, MedicalRecordDTO medicalRecord);
        void DeleteMedicalRecord(MedicalRecord medicalRecord);
        Task<MedicalRecordVetDTO> GetMedicalRecordsByAppointmentId(string appointmentId);
        string GenerateID();
    }
}
