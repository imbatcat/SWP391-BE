using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class CageService : ICageService
    {
        private readonly ICageRepository _cageService;

        public CageService(ICageRepository cageService)
        {
            _cageService = cageService;
        }

        public async Task CreateCage(CageDTO Cage)
        {
            var _cage = new Cage
            {
                CageNumber = Cage.CageNumber,
                IsOccupied = Cage.IsOccupied,

            };
            await _cageService.Create(_cage);
        }


        public void DeleteCage(Cage Cage)
        {
            throw new NotImplementedException();
        }

        public async Task<Cage?> GetCageByCondition(Expression<Func<Cage, bool>> expression)
        {
            return await _cageService.GetByCondition(expression);
        }

        public async Task<IEnumerable<Cage>> GetAllCages()
        {
            return await _cageService.GetAll();
        }


        public async Task UpdateCage(int id, CageDTO Cage)
        {
            var _cage = new Cage
            {
                CageId = id,
                IsOccupied = Cage.IsOccupied,
            };
            await _cageService.Update(_cage);
        }

    }
}
