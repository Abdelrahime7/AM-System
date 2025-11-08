using Application.Assisstants.Dto_s;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;

namespace Application.Assisstants.Mapper
{
    internal class AssisstantMapper : IEntityMapper<Assisstant, CreatAssisstantRequest, UpdateAssisstantRequest,
        AssisstantResponse>
    {
        public Assisstant ToEntity(CreatAssisstantRequest dto)
        {
            return new Assisstant
            {
               AssignedBy = (int)dto.AssignedBy,
             //  UserId = dto.UserId,


            };
        }

        public AssisstantResponse ToResponse(Assisstant entity)
        {
            return new AssisstantResponse
            {
                UserId = entity.UserId,
                AssignedBy = entity.AssignedBy,
            };
        }

        public void ToUpdateEntity(Assisstant entity, UpdateAssisstantRequest dto)
        {
            entity.UserId     = dto.UserId     ??  entity.UserId;
            entity.AssignedBy = dto.AssignedBy ?? entity.AssignedBy; 
            
        }
    }
}
