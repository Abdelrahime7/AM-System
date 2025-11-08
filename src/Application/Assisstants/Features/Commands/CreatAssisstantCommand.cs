using Application.Assisstants.Dto_s;
using Application.Assisstants.Dto_s.session;
using Application.Common.Models;
using Application.Interfaces.AssisstantInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserInterfaces;
using Domain.Entities;

namespace Application.Assisstants.Features.Commands
{
    partial class AssisstantCommands( IAssisstantRepository repository,
           IUserCommands  commands,
           IEntityMapper<Assisstant, CreatAssisstantRequest, UpdateAssisstantRequest,
           AssisstantResponse> mapper ) : IAssisstantCommands
    {
        private readonly IUserCommands _userCommands = commands;
        private readonly IAssisstantRepository _repository = repository;
        private readonly IEntityMapper<Assisstant, CreatAssisstantRequest,
            UpdateAssisstantRequest,   AssisstantResponse> _mapper=mapper;

        public async Task<Result<int>> CreateAssisstantAsync(CreatAssisstantSession request)
        {
            try
            {
                var User = await _userCommands.CreatUserAsync(request.userRequest);

                var Assisstant = _mapper.ToEntity(request.assisstantRequest);

                Assisstant.User = User.Value;
                await _repository.AddAsync(Assisstant);

                return Result<int>.Success(Assisstant.Id);

            }
            catch (Exception ex)
            {
                return Result<int>.Failure("Failed to add Assisstant");
            }
        }

      

      
    }
}
