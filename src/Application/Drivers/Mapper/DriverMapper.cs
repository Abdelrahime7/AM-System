using Application.Drivers.DTO_s;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;

namespace Application.Drivers.Mapper
{
    internal class DriverMapper : IEntityMapper<Driver, CreateDriverRequest, UpdateDriverRequest,
        DriverResponse>
    {
        public Driver ToEntity(CreateDriverRequest dto)
        {
            return new Driver
            {
                IsAvailable = dto.IsAvailable,
                IsLocal = dto.IsLocal,
              //  UserID = dto.UserID,


            };
        }

        public DriverResponse ToResponse(Driver entity)
        {
            return new DriverResponse
            {
                IsAvailable = entity.IsAvailable,
                IsLocal = entity.IsLocal,
                UserID = entity.UserID
            };
        }

        public void ToUpdateEntity(Driver entity, UpdateDriverRequest dto)
        {
            entity.IsLocal     = dto.IsLocal     ??  entity.IsLocal;
            entity.IsAvailable = dto.IsAvailable ?? entity.IsAvailable;
            entity.UserID      = dto.UserID      ?? entity.UserID;

        }
    }
}
