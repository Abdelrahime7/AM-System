using Application.Interfaces.OrderInterfaces;
using Domain.Entities;


namespace Application.Orders.Delivery
{
    public class LocalDriverDelivery : IDeliveryStrategy
    {
        public Task AssignAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
