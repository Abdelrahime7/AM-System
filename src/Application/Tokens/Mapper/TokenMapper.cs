using Application.AuditsLog.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Tokens.DTOs;
using Domain.Entities;


namespace Application.Tokens.Mapper;

public class TokenMapper : IEntityMapper<Token, CreateTokenRequest,
    UpdateTokenRequest, TokenResponse>
{
    public Token ToEntity(CreateTokenRequest dto)
    {
        throw new NotImplementedException();
    }

    public TokenResponse ToResponse(Token entity)
    {
        throw new NotImplementedException();
    }

    public void ToUpdateEntity(Token entity, UpdateTokenRequest dto)
    {
        throw new NotImplementedException();
    }
}