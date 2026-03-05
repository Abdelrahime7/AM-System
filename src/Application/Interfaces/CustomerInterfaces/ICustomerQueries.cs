using Application.Common.Models;
using Application.Customers.DTOs;

namespace Application.Interfaces.CustomerInterfaces;

public interface ICustomerQueries
{
    Task<Result<IEnumerable<CustomerResponse>>> GetAllAsync();
    Task<Result<CustomerResponse>> GetByIdAsync(int id);
    Task<Result<CustomerResponse>> GetByNameAsync(string name);
}