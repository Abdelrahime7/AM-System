using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;


namespace Application.Orders.Delivery
{
    public class LocalDriverDelivery (int DriverID, IOrderRepository repository) : IDeliveryStrategy
    {
        private readonly int _DriverID = DriverID;
       private readonly IOrderRepository _repository = repository;
     public async Task AssignAsync(Order order)
      {
        order.DriverId = _DriverID;
        _repository.Update(order);

      }
    }
}
