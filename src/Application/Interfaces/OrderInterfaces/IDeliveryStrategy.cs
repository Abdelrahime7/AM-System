

using Application.Common.Models;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.OrderInterfaces
{
    public interface IDeliverStrategy
    {
        Task AssignAsync(Order order);
    }
}
