using Application.Common.Models;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.OrderInterfaces;
using Application.OrderDetails.DTOs;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Domain.Entities;
using Domain.Enums;
using System.Collections.Generic;

namespace Application.Orders.Features.Queries;

public partial class OrderQueries
{
    public async Task<Result<bool>> ChangeOrderStatusAsync(Order order,OrderStatus status)
    {
        try
        {

            if (order == null)
                return Result<bool>.Failure("No order found");

            order.Status = status;
            _repository.Update(order);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Failed to change order status : {ex.Message}");
        }
    }


}