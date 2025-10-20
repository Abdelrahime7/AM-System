

using Application.Common.Models;
using Application.Drivers.DTO_s;
using Application.Interfaces.DriverInterfaces;

namespace Application.Drivers.features.Commands
{
    partial class DriverCommands : IDriverCommands
    {
        public Task<Result<int>> CreateDriverAsync(CreateDriverRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteDriverAsnc(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> UpdateDriverAsnc(UpdateDriverRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
