

using Application.Common.Models;
using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DriverInterfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserInterfaces;
using Domain.Entities;

namespace Application.Drivers.features.Commands
{
    partial class DriverCommands (IDriverRepository repository,
        IUserCommands commands,
         IEntityMapper<Driver,CreateDriverRequest,
             UpdateDriverRequest,DriverResponse> mapper) : IDriverCommands 
    {
        private readonly IDriverRepository _repository = repository;
        private readonly IUserCommands _userCommands = commands;
        private readonly IEntityMapper<Driver, CreateDriverRequest,
             UpdateDriverRequest, DriverResponse> _mapper= mapper;

        public async Task<Result<int>> CreateDriverAsync(CreatDriverSession request)
        {
            try
            {
               var User=  await _userCommands.CreatUserAsync(request.UserRequest);
                
                var Driver = _mapper.ToEntity(request.DriverRequest);

                Driver.User = User.Value;
                 await _repository.AddAsync(Driver);

                return Result<int>.Success(Driver.Id);

            }
            catch (Exception ex) {
                return Result<int>.Failure("Failed to add Driver");
            }

         }

      

      
    }
}
