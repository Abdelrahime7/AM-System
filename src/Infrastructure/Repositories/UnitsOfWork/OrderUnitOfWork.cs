using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Infrastructure.Data;

namespace Infrastructure.Repositories.UnitsOfWork
{
    public class OrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly AppDbContext _context;

        public IOrderRepository Orders { get; }
        public ICustomerRepository Customers { get; }
        public IDeliveryRepository Deliveries { get; }
        public ICustomizedOrderRepository CustomizedOrders { get; }

        public OrderUnitOfWork(
            AppDbContext context,
            IOrderRepository orders,
            ICustomerRepository customers,
            IDeliveryRepository deliveries,
            ICustomizedOrderRepository customizedOrders)
        {
            _context = context;
            Orders = orders;
            Customers = customers;
            Deliveries = deliveries;
            CustomizedOrders = customizedOrders;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
