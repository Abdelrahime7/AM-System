using Application.Common.Models;
using Application.Drivers.DTO_s;


namespace Application.Interfaces.DriverInterfaces
{
    public interface IDriverQueries
    {
        public Task<Result<DriverResponse>> GetById(int id);
        public Task<Result<IEnumerable<DriverResponse>>> GetAllDrivers();


    }
}
