using Application.Common.Models;
using Application.Interfaces.OrderInterfaces;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;

namespace Application.Orders.Features.Queries;

public partial class OrderQueries 
{
    public Task<Result<IEnumerable<ResponseSession>>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

   
}