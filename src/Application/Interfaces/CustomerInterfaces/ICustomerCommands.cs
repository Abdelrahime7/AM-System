using Application.Common.Models;
using Application.Customers.DTOs;

namespace Application.Interfaces.CustomerInterfaces;

public interface ICustomerCommands
{
    Task<Result<int>> CreateCustomerAsync(CreateCustomerRequest request);
    Task<Result<bool>> UpdateCustomerAsync(UpdateCustomerRequest request);
    Task<Result<bool>> DeleteCustomerAsync(int id);
}