using Application.Customers.DTOs;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;


namespace Application.Customers.Mapper;

public class CustomerMapper : IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse>
{
    public Customer ToEntity(CreateCustomerRequest dto)
    {
        return new Customer
        {
            FullName = dto.FullName,
            City = dto.City,
            Phone = dto.Phone,
            Address = dto.Address,
        };
    }

    public CustomerResponse ToResponse(Customer entity)
    {
        return new CustomerResponse
        {
            Id = entity.Id,
            FullName = entity.FullName,
            City = entity.City,
            Phone = entity.Phone,
            Address = entity.Address,
        };
    }

 

    public void ToUpdateEntity(Customer customer, UpdateCustomerRequest dto)
    {
        customer.FullName  = dto.FullName ?? customer.FullName;
        customer.City      = dto.City     ?? customer.City;
        customer.Phone     = dto.Phone    ?? customer.Phone;
        customer.Address   = dto.Address  ?? customer.Address;


    }
}