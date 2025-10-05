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
    public async Task<Result<OrderStatus>> GetOrderStatusAsync(Order order)
    {
        throw new NotImplementedException();
           
    }

   
}