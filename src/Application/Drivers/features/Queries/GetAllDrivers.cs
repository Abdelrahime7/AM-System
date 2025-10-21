using Application.Common.Models;
using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DriverInterfaces;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Domain.Entities;


namespace Application.Drivers.features.Queries
{
    partial class DriverQueries(IDriverRepository repository,
         IEntityMapper<Driver, CreateDriverRequest, UpdateDriverRequest,
             DriverResponse>  mapper, 
         IEntityMapper<User, CreateUserRequest, UpdateUserRequest,
             UserResponse> Usermapper
         ) : IDriverQueries

    {
        private readonly IDriverRepository _repository = repository;
        private readonly IEntityMapper<Driver, CreateDriverRequest,
            UpdateDriverRequest, DriverResponse>_mapper=mapper;
        private readonly IEntityMapper<User, CreateUserRequest, UpdateUserRequest,
             UserResponse> _Usermapper = Usermapper;
        public async Task<Result<IEnumerable<DriverSessionResponse>>> GetAllDrivers()
        {
            try {
                var Drivers = await _repository.GetAllAsync();
                if (!Drivers.Any())
                    return Result<IEnumerable<DriverSessionResponse>>.Failure("No Drivers Found");

                

                var responses = new List<DriverSessionResponse>();

                var tasks = Drivers.Select(driver => GetById(driver.Id)).ToList();
                var results = await Task.WhenAll(tasks);

                foreach (var result in results)
                {
                    responses.Add(result.Value);
                }

                return Result<IEnumerable<DriverSessionResponse>>.Success(responses);

            }
            catch (Exception ex) 
            {
                return Result<IEnumerable<DriverSessionResponse>>.Failure("Failled to fetch Drivers");
            }
        }

      
    }
}
