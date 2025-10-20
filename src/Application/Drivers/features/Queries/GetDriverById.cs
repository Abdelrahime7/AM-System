using Application.Common.Models;
using Application.Drivers.DTO_s;
using Application.Products.DTOs;

namespace Application.Drivers.features.Queries
{
    partial class DriverQueries
    {
        public async Task<Result<DriverResponse>> GetById(int id)
        {
            try
            {
                var driver = await _repository.GetByIdAsync(id);
                if (driver == null)
                    return Result<DriverResponse>.Failure("No driver Found");

                var response = _mapper.ToResponse(driver);
                return Result<DriverResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<DriverResponse>.Failure($"failed to fetch driver: {ex.Message}");
            }

        }

    }
}
