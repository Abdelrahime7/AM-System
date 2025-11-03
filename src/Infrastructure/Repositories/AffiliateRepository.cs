using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
namespace Infrastructure.Repositories
{
    public class AffiliateRepository(AppDbContext context) :GenericRepository<Affiliate>(context) ,IAffiliateRepository
    {
       
    }
}
