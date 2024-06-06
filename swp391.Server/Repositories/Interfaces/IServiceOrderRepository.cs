﻿using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;

namespace PetHealthcare.Server.Repositories.Interfaces
{
    public interface IServiceOrderRepository: IRepositoryBase<ServiceOrder>
    {
        Task CreateServiceOrder(ServiceOrderDTO order);
    }
}
