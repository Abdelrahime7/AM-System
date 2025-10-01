using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class DeliveryRepository(AppDbContext context) : GenericRepository<DeliveryIntegration>(context), IDeliveryRepository
{
    // Additional methods specific to Program can be added here
    
}
