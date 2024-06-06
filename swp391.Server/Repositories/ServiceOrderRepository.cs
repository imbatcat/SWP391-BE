using Microsoft.AspNetCore.Components.Sections;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Repositories.Interfaces;
using SQLitePCL;
using System.Linq.Expressions;

namespace PetHealthcare.Server.Repositories
{
    public class ServiceOrderRepository : IServiceOrderRepository
    {
        private readonly PetHealthcareDbContext context;
        public ServiceOrderRepository(PetHealthcareDbContext context)
        {
            this.context = context;
        }

        public string GenerateId()
        {
            string prefix = "SR-";
            string id = Nanoid.Generate(size: 8);
            return prefix + id;
        }
        public Task Create(ServiceOrder entity)
        {
            throw new NotImplementedException();
        }

        public async Task CreateServiceOrder(ServiceOrderDTO order)
        {
            string SoId = GenerateId();
            double priceSum=0;
            try
            {
                foreach (int serviceId in order.ServiceId)
                {
                    context.ServiceOrderDetails.Add(new ServiceOrderDetails
                    {
                        ServiceId = serviceId,
                        ServiceOrderId = SoId,
                    });
                    priceSum += context.Services.FirstOrDefault(s => s.ServiceId == serviceId).ServicePrice;
                    await SaveChanges();
                }
                ServiceOrder toCreateServiceOrder = new ServiceOrder
                {
                    ServiceOrderId = SoId,
                    OrderDate = order.OrderDate,
                    OrderStatus = "Pending",
                    MedicalRecordId = order.MedicalRecordId,
                    Price = priceSum,
                };
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.ToString());
                Console.WriteLine("priceSum error");
            }

        }

        

        public void Delete(ServiceOrder entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ServiceOrder>> GetAll()
        {
            return await context.ServiceOrders.Include(s => s.ServicePayment).Include(s => s.MedicalRecord).Include(s => s.ServiceOrderDetails).ToListAsync();
        }

        public async Task<ServiceOrder?> GetByCondition(Expression<Func<ServiceOrder, bool>> expression)
        {
            return await context.ServiceOrders.FirstOrDefaultAsync(expression);
        }

        public async Task SaveChanges()
        {
            
            await context.SaveChangesAsync();
        }

        public async Task Update(ServiceOrder entity)
        {
            var serviceOrder = await GetByCondition(a => a.ServiceOrderId == entity.ServiceOrderId);
            if(serviceOrder != null)
            {
                
            }
        }
    }
}
