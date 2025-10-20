using Application.Common.Models;
using Application.Drivers.DTO_s;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DriverInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;


namespace Application.Drivers.features.Queries
{
    partial class DriverQueries(IDriverRepository repository,
         IEntityMapper<Driver, CreateDriverRequest, UpdateDriverRequest,
             DriverResponse>  mapper) : IDriverQueries

    {
        private readonly IDriverRepository _repository = repository;
        IEntityMapper<Driver, CreateDriverRequest,
            UpdateDriverRequest, DriverResponse>_mapper=mapper;
        public async Task<Result<IEnumerable<DriverResponse>>> GetAllDrivers()
        {
            try {
                var Drivers = await _repository.GetAllAsync();
                if (!Drivers.Any())
                    return Result<IEnumerable<DriverResponse>>.Failure("No Drivers Found");

                var response = Drivers.ToList().Select(c => _mapper.ToResponse(c));
                return Result<IEnumerable<DriverResponse>>.Success(response);

            }
            catch (Exception ex) 
            {
                return Result<IEnumerable<DriverResponse>>.Failure("Failled to fetch Drivers");
            }
        }

      
    }
}
