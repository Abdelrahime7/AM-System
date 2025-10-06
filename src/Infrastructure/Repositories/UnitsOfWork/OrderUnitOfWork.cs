using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.OrderDetailInterfaces;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Infrastructure.Data;

namespace Infrastructure.Repositories.UnitsOfWork
{
    public class OrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly AppDbContext _context;

       
        public IOrderCommands Orders { get; }
        public ICustomerCommands Customers { get; }
        public ICustomizedOrderCommands CustomizedOrders { get; }
        public IOrderDetailCommands OrderDetails { get; }

        public IOrderRepository orderRepository => throw new NotImplementedException();

        public OrderUnitOfWork(
            AppDbContext context,
            IOrderCommands orders,
            ICustomerCommands customers,
            ICustomizedOrderCommands customizedOrders,
            IOrderDetailCommands orderDetails)
        {
            _context = context;
            Orders = orders;
            Customers = customers;
            CustomizedOrders = customizedOrders;
            OrderDetails = orderDetails;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }


}
