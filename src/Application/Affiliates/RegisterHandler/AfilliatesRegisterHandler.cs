
using Application.Affiliates.DTO_s;
using Application.Affiliates.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.AffiliateInterfaces;
using Application.Interfaces.RegisterHandler;
using Application.RoleRequeste;
using Domain.Enums;


namespace Application.Affiliates.RegisterHandler
{

    public class AfilliatesRegisterHandler : IRegisterHandler
    {
        private readonly IAffiliateCommands _affiliateCommands;

        public string RoleName => UserRole.Affiliate.ToString();

        public AfilliatesRegisterHandler(IAffiliateCommands affiliateCommands)
        {
            _affiliateCommands = affiliateCommands;
        }


        public async Task<Result<int>> RegisterAsync(CreateRoleSession request)
        {
            var affiliateReq = request.RoleRequest as CreateAffiliateRequest;
            if (affiliateReq == null)
                return Result<int>.Failure("Invalid Affiliate request payload");

            var session = new CreatAffiliateSession
            {
                UserRequest = request.UserRequest,
                AffiliateRequest = affiliateReq
            };
            return await _affiliateCommands.CreateAffiliateAsync(session);
        }


    }



    }


