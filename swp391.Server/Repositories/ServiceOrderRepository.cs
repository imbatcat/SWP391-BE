using Microsoft.AspNetCore.Components.Sections;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.ServiceOrderDTO;
using PetHealthcare.Server.APIs.DTOS.ServiceOrderDTOs;
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

                ServiceOrder toCreateServiceOrder = new ServiceOrder
                {
                    ServiceOrderId = SoId,
                    OrderDate = order.OrderDate,
                    OrderStatus = "Pending",
                    MedicalRecordId = order.MedicalRecordId,
                    Price = context.Services.Where(s => order.ServiceId.Contains(s.ServiceId)).Sum(s => s.ServicePrice),
                };
                context.ServiceOrders.Add(toCreateServiceOrder);
                foreach (int serviceId in order.ServiceId)
                {
                    context.ServiceOrderDetails.Add(new ServiceOrderDetails
                    {
                        ServiceId = serviceId,
                        ServiceOrderId = SoId,
                    });
                    
                }
                await SaveChanges();
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.ToString());
                Console.WriteLine("Error");
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
        
        }
        public async Task UpdateServiceOrder(string serviceOrderId, List<int> ServiceId)
        {
            ServiceOrder? toUpdateServiceOrder = await context.ServiceOrders.FirstOrDefaultAsync(s => s.ServiceOrderId.Equals(serviceOrderId));
            if (toUpdateServiceOrder == null)
            {
                return;
            } else
            {
                //remove old ServiceOrderDetail than replace it by the new ones
                var toRemoveOrderDetails = context.ServiceOrderDetails.Where(s => s.ServiceOrderId.Equals(toUpdateServiceOrder.ServiceOrderId)).ToList();
                context.ServiceOrderDetails.RemoveRange(toRemoveOrderDetails);
                //Add new ones
                double newPrice = 0;
                foreach (int serviceId in ServiceId)
                {
                    context.ServiceOrderDetails.Add(new ServiceOrderDetails
                    {
                        ServiceId = serviceId,
                        ServiceOrderId = serviceOrderId,
                    });
                    newPrice += context.Services.Find(serviceId).ServicePrice;
                }
                toUpdateServiceOrder.Price = newPrice; //new Price
                await SaveChanges();
            }
        }
      
        public async Task<IEnumerable<GetAllServiceOrderForStaff>> GetAllServiceOrderForStaff()
        {
            //    public string ServiceOrderId { get; set; }
            //public double Price { get; set; }
            //public DateOnly OrderDate { get; set; }
            //public string OrderStatus { get; set; }
            //public string customerName { get; set; }
            var orderServiceList = context.ServiceOrders.Include("MedicalRecord.Appointment.Account");

            List<GetAllServiceOrderForStaff> ServiceOrderForStaff = new List<GetAllServiceOrderForStaff>();
            foreach (ServiceOrder order in orderServiceList)
            {
                ServiceOrderForStaff.Add(new GetAllServiceOrderForStaff
                {
                    ServiceOrderId = order.ServiceOrderId,
                    Price = order.Price,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    customerName = order.MedicalRecord.Appointment.Account.FullName,
                });
            }
            return ServiceOrderForStaff;
        }

        public async Task<bool> savePaymentService(string serviceOrderId, string paymentMethod)
        {
            string servicePaymentId = "SP-" + Nanoid.Generate(size: 8);
            ServiceOrder? serviceOrder = await context.ServiceOrders.FindAsync(serviceOrderId);
            if(serviceOrder != null)
            {
                serviceOrder.OrderStatus = "Paid";
                ServicePayment toAddServicePayment = new ServicePayment
                {
                    ServicePaymentId = servicePaymentId,
                    PaymentMethod = paymentMethod,
                    PaymentDate = DateOnly.FromDateTime(DateTime.Today),
                    ServiceOrderId = serviceOrderId,
                };
                context.ServicePayments.Add(toAddServicePayment);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
