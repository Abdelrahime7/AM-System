

using Application.Common.Models;
using Application.Tokens.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.TokenInterfaces
{
    public interface ITokenQueries
    {
        Task<Result<IEnumerable<TokenResponse>>> GetAllTokensAsync();
        Task<Result<TokenResponse>> GetTokenByIDAsync(int id);
     

    }
}
