using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public void CreateCage(CageDTO Cage)
        {
            var _cage = new Cage
            {
                CageNumber = Cage.CageNumber,
                IsOccupied = Cage.IsOccupied,

            };
            _cageService.Create(_cage);
        }


        public void DeleteCage(Cage Cage)
        {
            throw new NotImplementedException();
        }

        public Cage? GetCageByCondition(Expression<Func<Cage, bool>> expression)
        {
            return null;
        }

        public IEnumerable<Cage> GetAllCages()
        {
            return null;
        }


        public void UpdateCage(int id, CageDTO Cage)
        {
            var _cage = new Cage
            {
                CageId = id,
                IsOccupied = Cage.IsOccupied,
            };
            _cageService.Update(_cage);
        }

    }
}
