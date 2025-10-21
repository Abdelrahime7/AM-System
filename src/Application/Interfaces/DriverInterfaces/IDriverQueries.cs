using Application.Common.Models;
using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;


namespace Application.Interfaces.DriverInterfaces
{
    public interface IDriverQueries
    {
        public Task<Result<DriverSessionResponse>> GetById(int id);
        public Task<Result<IEnumerable<DriverSessionResponse>>> GetAllDrivers();


    }
}
