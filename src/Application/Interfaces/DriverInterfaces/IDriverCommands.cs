using Application.Common.Models;
using Application.Drivers.DTO_s;

namespace Application.Interfaces.DriverInterfaces
{
    public interface IDriverCommands
    {
        public Task<Result<int>> CreateDriverAsync(CreateDriverRequest request);
        public Task<Result<bool>> DeleteDriverAsnc(int Id);
        public  Task <Result<bool>> UpdateDriverAsnc(UpdateDriverRequest request);
    }
}
