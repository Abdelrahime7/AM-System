using Application.Common.Models;
using Domain.Entities;


namespace Application.Drivers.features.Commands
{
    partial class DriverCommands

    {
        public Result<bool> ChangeDriverAvaillability(Driver driver)
        {

            try
            {
                if (driver == null)
                    return Result<bool>.Failure("no driver ");
               if( driver.IsAvailable )
                    driver.IsAvailable = false;
               else driver.IsAvailable = true;

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
