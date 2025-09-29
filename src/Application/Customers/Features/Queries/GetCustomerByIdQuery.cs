using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Customers.Features.Queries;

public partial class CustomerQueries(
    ICustomerRepository repository,
    IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse> mapper)
    : ICustomerQueries
{
    private readonly ICustomerRepository _repository = repository;
    private readonly IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse> _mapper = mapper;

    public async Task<Result<CustomerResponse>> GetByIdAsync(int id)
    {
        try
        {
            var customer = await _repository.GetByIdAsync(id);
            if(customer == null)
                return Result<CustomerResponse>.Failure("No Customer Found");

            var response = _mapper.ToResponse(customer);
            return Result<CustomerResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<CustomerResponse>.Failure($"failed to fetch customer: {ex.Message}");
        }
    }
}