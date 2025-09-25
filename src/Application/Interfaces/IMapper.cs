

namespace Application.Interfaces
{
   public interface IEntityMapper<TEntity, TCreateDto, TUpdateDto, TResponseDto>
{
    TEntity ToEntity(TCreateDto dto);
    TResponseDto ToResponse(TEntity entity);
}
}
