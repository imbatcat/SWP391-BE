using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface IAdmissionRecordService
    {
        Task<IEnumerable<AdmissionRecord>> GetAll();
        Task<AdmissionRecord?> GetAdmissionRecordByCondition(Expression<Func<AdmissionRecord, bool>> expression);
        Task CreateAdmissionRecord(AdmissionRecordDTO entity);
        Task UpdateAdmissionRecord(string id, AdmissionRecordDTO entity);
        void DeleteAdmissionRecord(AdmissionRecord entity);
    }
}
