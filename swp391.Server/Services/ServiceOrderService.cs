﻿using Microsoft.AspNetCore.Components.Sections;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Services
{
    public class ServiceOrderService : IServiceOrderService
    {
        private readonly IServiceOrderRepository _serviceOrderRepo;
        public ServiceOrderService(IServiceOrderRepository serviceRepo)
        {
            _serviceOrderRepo = serviceRepo;
        }
        public async Task CreateServiceOrder(ServiceOrderDTO orderDTO)
        {
            await _serviceOrderRepo.CreateServiceOrder(orderDTO);
        }

        public Task<IEnumerable<ServiceOrder>> GetAllServiceOrder()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceOrder>> GetAllServiceOrderByMedId(string medId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceOrder> GetServiceOrderById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderStatus(string orderStatus)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateServiceOrder(string id, List<int> serviceIdList)
        {
            await _serviceOrderRepo.UpdateServiceOrder(id, serviceIdList);
        }
    }
}
