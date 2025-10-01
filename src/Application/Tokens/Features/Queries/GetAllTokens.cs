using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.TokenInterfaces;
using Application.Tokens.DTOs;

namespace Application.Tokens.Features.Queries;

public partial class TokenQueries : ITokenQueries
{
    public Task<Result<IEnumerable<TokenResponse>>> GetAllTokensAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<TokenResponse>> GetTokenByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
}