using Application.Assisstants.Dto_s;
using Application.Assisstants.Dto_s.session;
using Application.Common.Models;
using Application.Interfaces.AssisstantInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Domain.Entities;

namespace Application.Assisstants.Features.Queries
{
    partial class AssisstantQueries(IAssisstantRepository repository,
         IEntityMapper<Assisstant,CreatAssisstantRequest,UpdateAssisstantRequest,
             AssisstantResponse> mapper,
          IEntityMapper<User, CreateUserRequest, UpdateUserRequest,
             UserResponse> Usermapper) : IAssisstantQueries
    {

        private readonly IAssisstantRepository _repository= repository;
        private readonly IEntityMapper<Assisstant, CreatAssisstantRequest, UpdateAssisstantRequest,
             AssisstantResponse> _mapper = mapper;
        IEntityMapper<User, CreateUserRequest, UpdateUserRequest,
              UserResponse> _Usermapper = Usermapper;

      
        public async Task<Result<AssisstantSessionResponse>> GetById(int id)
        {
            try
            {
                var Assisstant = await _repository.GetByIdAsync(id);
                if (Assisstant == null)
                    return Result<AssisstantSessionResponse>.Failure("No Assisstant Found");

                var AssisstantResponse = _mapper.ToResponse(Assisstant);
                var UserResponse = _Usermapper.ToResponse(Assisstant.User);

                var response = new AssisstantSessionResponse
                {
                    UserResponse = UserResponse,
                    AssisstantResponse = AssisstantResponse
                };

                return Result<AssisstantSessionResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<AssisstantSessionResponse>.Failure($"failed to fetch Assisstant: {ex.Message}");
            }
        }
    }
}
