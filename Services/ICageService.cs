using PetHealthcareSystem.APIs.DTOS;
using PetHealthcareSystem.DTOS;
using PetHealthcareSystem.Models;
using System.Linq.Expressions;

namespace PetHealthcareSystem.Services
{
    public interface ICageService
    {
        IEnumerable<Cage> GetAllCages();
        Cage? GetCageByCondition(Expression<Func<Cage, bool>> expression);
        void CreateCage(CageDTO Account);
        void UpdateCage(int id, CageDTO Account);
        void DeleteCage(Cage Account);
    }
}
