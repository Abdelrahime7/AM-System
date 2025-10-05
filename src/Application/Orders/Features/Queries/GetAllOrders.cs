using Application.Common.Models;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.OrderInterfaces;
using Application.OrderDetails.DTOs;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using System.Collections.Generic;

namespace Application.Orders.Features.Queries;

public partial class OrderQueries 
{
    public async Task<Result<IEnumerable<ResponseSession>>> GetAllOrdersAsync()
    {
        try
        {
            var Orders = await _repository.GetAllAsync();
            if (Orders.Count() < 0)
            {
                return Result<IEnumerable<ResponseSession>>.Failure("No Orders found");
            }

            var orderResponses = new List<ResponseSession>();
            foreach (var order in Orders)
            {

                var response = await GetOrderByIDAsync(order.Id);
                orderResponses.Add(response.Value);

            }

            return Result<IEnumerable<ResponseSession>>.Success(orderResponses);
        }
        catch(Exception ex) 
        {
            return Result<IEnumerable<ResponseSession>>.Failure($"Failed to fetch orders: {ex.Message}");
        }

    }

   
}