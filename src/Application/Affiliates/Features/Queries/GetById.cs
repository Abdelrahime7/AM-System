using Application.Affiliates.DTO_s;
using Application.Affiliates.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.AffiliateInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Domain.Entities;

namespace Application.Affiliates.Features.Queries
{
    partial class AffiliateQueries(IAffiliateRepository repository,
         IEntityMapper<Affiliate,CreateAffiliateRequest,UpdateAffiliateRequest,
             AffiliateResponse> mapper,
          IEntityMapper<User, CreateUserRequest, UpdateUserRequest,
             UserResponse> Usermapper) : IAffiliateQueries
    {

        private readonly IAffiliateRepository _repository= repository;
        private readonly IEntityMapper<Affiliate, CreateAffiliateRequest, UpdateAffiliateRequest,
             AffiliateResponse> _mapper = mapper;
        IEntityMapper<User, CreateUserRequest, UpdateUserRequest,
              UserResponse> _Usermapper = Usermapper;

      
        public async Task<Result<AffiliateSessionResponse>> GetById(int id)
        {
            try
            {
                var Affiliate = await _repository.GetByIdAsync(id);
                if (Affiliate == null)
                    return Result<AffiliateSessionResponse>.Failure("No Affiliate Found");

                var AffiliateResponse = _mapper.ToResponse(Affiliate);
                var UserResponse = _Usermapper.ToResponse(Affiliate.user);

                var response = new AffiliateSessionResponse
                {
                    UserResponse = UserResponse,
                    AffiliateResponse = AffiliateResponse
                };

                return Result<AffiliateSessionResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<AffiliateSessionResponse>.Failure($"failed to fetch Affiliate: {ex.Message}");
            }
        }
    }
}
