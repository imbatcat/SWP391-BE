using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services.Interfaces
{
    public interface ICageService
    {
        IEnumerable<Cage> GetAllCages();
        Cage? GetCageByCondition(Expression<Func<Cage, bool>> expression);
        void CreateCage(CageDTO Cage);
        void UpdateCage(int id, CageDTO Cage);
        void DeleteCage(Cage Cage);
    }
}
