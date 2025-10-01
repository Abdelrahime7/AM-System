using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.TokenInterfaces;
using Application.Tokens.DTOs;
using Domain.Entities;

namespace Application.Tokens.Features.Commands;

public partial class TokenCommands : ITokenCommands
{
    public Task<Result<int>> CreatTokenAsync(CreateTokenRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteTokenAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateTokenAsync(UpdateTokenRequest request)
    {
        throw new NotImplementedException();
    }
}
