using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.OrderDetailInterfaces;
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
        IOrderCommands Orders { get; }
        ICustomerCommands Customers { get; }
        ICustomizedOrderCommands CustomizedOrders { get; }
        IOrderDetailCommands OrderDetails { get; }

        Task<int> SaveChangesAsync();
    }


}
