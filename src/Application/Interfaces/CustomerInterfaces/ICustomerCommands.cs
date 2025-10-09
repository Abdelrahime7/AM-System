using Application.Common.Models;
using Application.Customers.DTOs;
using Domain.Entities;

namespace Application.Interfaces.CustomerInterfaces;

public interface ICustomerCommands
{
    Task<Result<Customer>> CreateCustomerAsync(CreateCustomerRequest request);
    Task<Result<bool>> UpdateCustomerAsync(UpdateCustomerRequest request);
    Task<Result<bool>> DeleteCustomerAsync(int id);
}