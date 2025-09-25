

namespace Application.Interfaces
{
   public interface IEntityMapper<TEntity, TCreateDto, TUpdateDto, TResponseDto>
{
    TEntity ToEntity(TCreateDto dto);
    TEntity ToUpdateEntity(TUpdateDto dto);
    TResponseDto ToResponse(TEntity entity);
}
}
