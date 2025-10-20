

using Application.Common.Models;
using Application.Drivers.DTO_s;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DriverInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Drivers.features.Commands
{
    partial class DriverCommands (IDriverRepository repository,
         IEntityMapper<Driver,CreateDriverRequest,
             UpdateDriverRequest,DriverResponse> mapper) : IDriverCommands 
    {
        private readonly IDriverRepository _repository = repository;
        private readonly IEntityMapper<Driver, CreateDriverRequest,
             UpdateDriverRequest, DriverResponse> _mapper= mapper;

        public async Task<Result<int>> CreateDriverAsync(CreateDriverRequest request)
        {
            try
            {
                var Driver = _mapper.ToEntity(request);
                 await _repository.AddAsync(Driver);

                return Result<int>.Success(Driver.Id);

            }
            catch (Exception ex) {
                return Result<int>.Failure("Failed to add Driver");
            }

         }

      

      
    }
}
