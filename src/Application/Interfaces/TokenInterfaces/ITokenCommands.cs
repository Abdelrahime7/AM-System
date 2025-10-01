

using Application.Common.Models;
using Application.Tokens.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.TokenInterfaces
{
    public interface ITokenCommands
    {
        Task<Result<int>> CreatTokenAsync(CreateTokenRequest request);
        Task<Result<bool>> DeleteTokenAsync(int ID);
        Task<Result<bool>> UpdateTokenAsync(UpdateTokenRequest request);
       

    }
}
