namespace Application.Interfaces.Common.Mappers
{
   public interface IEntityMapper<TEntity, TCreateDto, TUpdateDto, TResponseDto>
{
    TEntity ToEntity(TCreateDto dto);
    TResponseDto ToResponse(TEntity entity);
}
}
