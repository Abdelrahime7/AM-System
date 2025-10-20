using Application.Common.Models;
using Application.Drivers.DTO_s;


namespace Application.Drivers.features.Commands
{
    partial class DriverCommands
    {
        public async Task<Result<bool>> UpdateDriverAsnc(UpdateDriverRequest request)
        {
            try
            {
                var Driver = await _repository.GetByIdAsync(request.Id);
                if (Driver == null)
                    return Result<bool>.Failure("Driver not found");

                _mapper.ToUpdateEntity(Driver, request);
                _repository.Update(Driver);
                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure($"Failed to update Driver: {e.Message}");
            }
        }
    }
}
