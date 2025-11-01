using Application.Admins.Dto_s;
using Application.Admins.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.AdminInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserInterfaces;
using Domain.Entities;

namespace Application.Admins.Features.Commands
{
    partial class AdminCommands( IAdminRepository repository,
           IUserCommands  commands,
           IEntityMapper<Admin, CreateAdminRequest, UpdateAdminRequest,
           AdminResponse> mapper ) : IAdminCommands
    {
        private readonly IUserCommands _userCommands = commands;
        private readonly IAdminRepository _repository = repository;
        private readonly IEntityMapper<Admin, CreateAdminRequest,
            UpdateAdminRequest,   AdminResponse> _mapper=mapper;

        public async Task<Result<int>> CreateAdminAsync(CreatAdminSession request)
        {
            try
            {
                var User = await _userCommands.CreatUserAsync(request.UserRequest);

                var Admin = _mapper.ToEntity(request.AdminRequest);

                Admin.user = User.Value;
                await _repository.AddAsync(Admin);

                return Result<int>.Success(Admin.Id);

            }
            catch (Exception ex)
            {
                return Result<int>.Failure("Failed to add Admin");
            }
        }

      

      
    }
}
