using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Customers.Features.Commands;

public partial class CustomerCommands(
    ICustomerRepository repository,
    IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse> mapper)
    : ICustomerCommands
{
    private readonly ICustomerRepository _repository = repository;
    private readonly IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse> _mapper = mapper;
    
    public async Task<Result<Customer>> CreateCustomerAsync(CreateCustomerRequest request)
    {
        try
        {
          
            var customer = _mapper.ToEntity(request);
            await _repository.AddAsync(customer);
          
            return Result<Customer>.Success(customer);
        }
        catch (Exception ex)

        {
           
            Console.WriteLine($"Exception: {ex}");
            return Result<Customer>.Failure($"Error creating customer: {ex.Message}");
        }
    }
}
