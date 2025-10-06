using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UnitOfWorks
{

    public interface IOrderUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }
        IDeliveryRepository Deliveries { get; }
        ICustomizedOrderRepository CustomizedOrders { get; }

        
        Task<int> SaveChangesAsync();
    }

}
