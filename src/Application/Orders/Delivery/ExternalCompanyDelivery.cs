using Application.Interfaces.OrderInterfaces;
using Domain.Entities;

namespace Application.Orders.Delivery
{
    internal class ExternalCompanyDelivery : IDeliveryStrategy
    {
        public Task AssignAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
