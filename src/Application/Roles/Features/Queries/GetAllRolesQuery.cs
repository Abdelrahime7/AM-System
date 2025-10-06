using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.RoleInterfaces;
using Application.Interfaces.Repositories;
using Application.Roles.DTOs;
using Domain.Entities;

namespace Application.Roles.Features.Queries;

public partial class RoleQueries(
    IRoleRepository repository,
    IEntityMapper<Role, CreateRoleRequest, UpdateRoleRequest, RoleResponse> mapper)
    : IRoleQueries
{
    private readonly IRoleRepository _repository = repository;
    private readonly IEntityMapper<Role, CreateRoleRequest, UpdateRoleRequest, RoleResponse> _mapper = mapper;

    public async Task<Result<IEnumerable<RoleResponse>>> GetAllRolesAsync()
    {
        try
        {
            var roles = await _repository.GetAllAsync();
            if (!roles.Any())
                return Result<IEnumerable<RoleResponse>>.Failure("No roles found");
            
            var response = roles.ToList().Select(c => _mapper.ToResponse(c));
            return Result<IEnumerable<RoleResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<RoleResponse>>.Failure($"Failed to fetch roles: {ex.Message}");
        }
    }
}