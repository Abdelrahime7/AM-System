using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;


namespace Application.Orders.Delivery
{
    public class LocalDriverDelivery (IOrderRepository repository,IUserRepository userRepository) : ILocalDeliveryStrategy
    {
       private readonly IOrderRepository _repository = repository;
        private readonly IUserRepository _userRepository = userRepository;
      

        public async Task AssignAsync(Order order)
      {
            var driver = await _userRepository.GetDriver();

          // it will be audited
        order.DriverId = driver.Id;
        _repository.Update(order);

      }
    }
}
