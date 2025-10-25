using Application.Common.Models;
using Application.Drivers.DTO_s;
using Domain.Entities;
using System.Threading.Tasks;


namespace Application.Drivers.features.Commands
{
    partial class DriverCommands

    {
        public async Task<Result<bool>> ChangeDriverAvaillability(ChangeAvailability availability)
        {

            try
            {
                var driver = await _repository.GetByIdAsync(availability.DriverID);
                if (driver == null)
                    return Result<bool>.Failure("no driver ");

                driver.IsAvailable = availability.Availability;
             

                    _repository.Update(driver);
                return Result<bool>.Success(true);

            }
            catch(Exception ex)
            {
                return Result<bool>.Failure($"Failed to Change Driver Availlability {ex.Message} ");
            }

        }


    }
}
