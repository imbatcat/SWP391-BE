﻿using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetHealthcareDbContext context;

        public PetRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }

        public async Task SaveChanges()
        {
           await context.SaveChangesAsync();
        }
        public async Task Create(Pet entity)
        {
            var res = await ConfirmPetIdentity(entity.AccountId, entity);
            // if false then throw new exception
            await context.Pets.AddAsync(entity);
            await  SaveChanges();
        }

        public void Delete(Pet entity)
        {
            context.Pets.Remove(entity);
        }

        public async Task< IEnumerable<Pet>> GetAll()
        {
            return await context.Pets.ToListAsync();
        }

        public async Task< Pet?> GetByCondition(Expression<Func<Pet, bool>> expression)
        {
            return await context.Pets.FirstOrDefaultAsync(expression);
        }


        public async Task Update(Pet entity)
        {
            var pet =await GetByCondition(e => e.PetId == entity.PetId);
            if (pet != null)
            {
                context.Entry(pet).State = EntityState.Modified;
                pet.PetName = entity.PetName;
                pet.Description = entity.Description;
                pet.PetAge = entity.PetAge;
                pet.VaccinationHistory = entity.VaccinationHistory;
                await SaveChanges();
            }
        }
        public Pet? GetPetById(string id)
        {
            return context.Pets.FirstOrDefault(a => a.PetId == id);
        }

        public async Task<IEnumerable<Pet>> GetAccountPets(string id)
        {
            return await context.Pets.Where(p => p.AccountId == id).ToListAsync();
        }

        public async Task<bool> ConfirmPetIdentity(string AccountId, Pet newPet)
        {
            // newPet's name, breed and isCat must not match any pets of this owner in the database

            //get list of pet by accound id, then check if theres any pet in the database matches the 
            //mentioned props of newPets, if yes then return false, true if otherwise. 
            return false;
        }
    }
}
