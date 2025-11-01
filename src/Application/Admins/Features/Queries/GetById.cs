using Application.Admins.Dto_s;
using Application.Admins.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.AdminInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Domain.Entities;

namespace Application.Admins.Features.Queries
{
    partial class AdminQueries(IAdminRepository repository,
         IEntityMapper<Admin,CreateAdminRequest,UpdateAdminRequest,
             AdminResponse> mapper,
          IEntityMapper<User, CreateUserRequest, UpdateUserRequest,
             UserResponse> Usermapper) : IAdminQueries
    {

        private readonly IAdminRepository _repository= repository;
        private readonly IEntityMapper<Admin, CreateAdminRequest, UpdateAdminRequest,
             AdminResponse> _mapper = mapper;
        IEntityMapper<User, CreateUserRequest, UpdateUserRequest,
              UserResponse> _Usermapper = Usermapper;

      
        public async Task<Result<AdminSessionResponse>> GetById(int id)
        {
            try
            {
                var Admin = await _repository.GetByIdAsync(id);
                if (Admin == null)
                    return Result<AdminSessionResponse>.Failure("No Admin Found");

                var AdminResponse = _mapper.ToResponse(Admin);
                var UserResponse = _Usermapper.ToResponse(Admin.user);

                var response = new AdminSessionResponse
                {
                    UserResponse = UserResponse,
                    AdminResponse = AdminResponse
                };

                return Result<AdminSessionResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<AdminSessionResponse>.Failure($"failed to fetch Admin: {ex.Message}");
            }
        }
    }
}
