using Application.Admins.Dto_s;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;

namespace Application.Admins.Mapper
{
    internal class AdminMapper : IEntityMapper<Admin, CreateAdminRequest, UpdateAdminRequest,
        AdminResponse>
    {
        public Admin ToEntity(CreateAdminRequest dto)
        {
            return new Admin
            {
                access = (Domain.Enums.AccessLevels)dto.levels


            };
        }

        public AdminResponse ToResponse(Admin entity)
        {
            return new AdminResponse
            {
                levels = entity.access,
                UserID = entity.UserID
            };
        }

        public void ToUpdateEntity(Admin entity, UpdateAdminRequest dto)
        {
            entity.access     = dto.levels     ??  entity.access;
            
        }
    }
}
