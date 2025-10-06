using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.RoleInterfaces;
using Application.Roles.DTOs;
using Domain.Entities;

namespace Application.Roles.Features.Commands;

public partial class RoleCommands(
    IRoleRepository repository,
    IEntityMapper<Role, CreateRoleRequest, UpdateRoleRequest, RoleResponse> mapper)
    : IRoleCommands
{
    private readonly IRoleRepository _repository = repository;
    private readonly IEntityMapper<Role, CreateRoleRequest, UpdateRoleRequest, RoleResponse> _mapper = mapper;

    public async Task<Result<int>> CreateRoleAsync(CreateRoleRequest request)
    {
        try
        {
            // Check if role type already exists
            var existingRole = await _repository.GetByRoleTypeAsync(request.RoleType);
            if (existingRole != null)
                return Result<int>.Failure($"Role type '{request.RoleType}' already exists");

            var role = _mapper.ToEntity(request);
            await _repository.AddAsync(role);
            return Result<int>.Success(role.Id);
        }
        catch (Exception e)
        {
            return Result<int>.Failure($"Error creating role: {e.Message}");
        }
    }
}
