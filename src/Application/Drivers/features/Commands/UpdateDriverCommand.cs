using Application.Common.Models;
using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;
using Domain.Entities;


namespace Application.Drivers.features.Commands
{
    partial class DriverCommands
    {
        public async Task<Result<bool>> UpdateDriverAsnc(UpdateDriverSession request)
        {
            try
            {
                var Driver = await _repository.GetByIdAsync(request.DriverRequest.Id);
               
                if (Driver == null)
                    return Result<bool>.Failure("Driver not found");

                if (request.UserRequest!=null)
                {
                  await  _userCommands.UpdateUserAsync(request.UserRequest);
                }
                _mapper.ToUpdateEntity(Driver, request.DriverRequest);

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
