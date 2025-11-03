using Application.Affiliates.DTO_s;
using Application.Affiliates.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.AffiliateInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserInterfaces;
using Domain.Entities;

namespace Application.Affiliates.Features.Commands
{
    partial class AffiliateCommands( IAffiliateRepository repository,
           IUserCommands  commands,
           IEntityMapper<Affiliate, CreateAffiliateRequest, UpdateAffiliateRequest,
           AffiliateResponse> mapper ) : IAffiliatCommands
    {
        private readonly IUserCommands _userCommands = commands;
        private readonly IAffiliateRepository _repository = repository;
        private readonly IEntityMapper<Affiliate, CreateAffiliateRequest,
            UpdateAffiliateRequest,   AffiliateResponse> _mapper=mapper;

        public async Task<Result<int>> CreateAffiliateAsync(CreatAffiliateSession request)
        {
            try
            {
                var User = await _userCommands.CreatUserAsync(request.UserRequest);

                var Affiliate = _mapper.ToEntity(request.AffiliateRequest);

                Affiliate.user = User.Value;
                await _repository.AddAsync(Affiliate);

                return Result<int>.Success(Affiliate.Id);

            }
            catch (Exception ex)
            {
                return Result<int>.Failure("Failed to add Affiliate");
            }
        }

      

      
    }
}
