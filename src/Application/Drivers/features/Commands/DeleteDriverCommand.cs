using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.features.Commands
{
    partial class DriverCommands
    {
        public async Task<Result<bool>> DeleteDriverAsnc(int Id)
        {
            try
            {
                var Driver = await _repository.GetByIdAsync(Id);
                if (Driver == null)
                    return Result<bool>.Failure("Driver Not Found");

                _repository.Delete(Driver);
                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure($"Error Deleting Driver: {e.Message}");
            }
        }
    }
}
