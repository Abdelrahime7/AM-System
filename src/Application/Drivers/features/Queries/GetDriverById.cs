using Application.Common.Models;
using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;
using Application.Products.DTOs;

namespace Application.Drivers.features.Queries
{
    partial class DriverQueries
    {
        public async Task<Result<DriverSessionResponse>> GetById(int id)
        {
            try
            {
                var driver = await _repository.GetByIdAsync(id);
                if (driver == null)
                    return Result<DriverSessionResponse>.Failure("No driver Found");

                var driverResponse = _mapper.ToResponse(driver);
                var UserResponse = _Usermapper.ToResponse(driver.User);

                var response = new DriverSessionResponse
                {
                    UserResponse = UserResponse,
                    DriverResponse = driverResponse
                };

                return Result<DriverSessionResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<DriverSessionResponse>.Failure($"failed to fetch driver: {ex.Message}");
            }

        }

    }
}
