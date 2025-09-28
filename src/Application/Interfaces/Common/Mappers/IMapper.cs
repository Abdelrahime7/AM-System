namespace Application.Interfaces.Common.Mappers
{
   public interface IEntityMapper<TEntity, TCreateDto, TUpdateDto, TResponseDto>
{
    TEntity ToEntity(TCreateDto dto);
    TEntity ToUpdateEntity(TUpdateDto dto);
    TResponseDto ToResponse(TEntity entity);
}
}
