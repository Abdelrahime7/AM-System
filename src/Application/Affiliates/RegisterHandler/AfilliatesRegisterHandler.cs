using Application.Admins.Dto_s;
using Application.Admins.DTO_s.session;
using Application.Affiliates.DTO_s;
using Application.Affiliates.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.AdminInterfaces;
using Application.Interfaces.AffiliateInterfaces;
using Application.Interfaces.RegisterHandler;
using Application.RoleRequeste;


namespace Application.Affiliates.RegisterHandler
{
  
    public class AfilliatesRegisterHandler : IRegisterHandler
    {
        private readonly IAffiliateCommands  _affiliateCommands;

        public string RoleName => "Admin";

        public AfilliatesRegisterHandler(IAffiliateCommands affiliateCommands)
        {
            _affiliateCommands = affiliateCommands;
        }

        public async Task<Result<int>> RegisterAsync(CreateRoleSession request)
        {
            // Cast the role-specific payload
            var adminReq = request.RoleRequest as CreateAffiliateRequest;
            if (adminReq == null)
                return Result<int>.Failure("Invalid Admin request payload");

            // Wrap into your existing CreateAdminSession
            var session = new CreatAffiliateSession
            {
                UserRequest = request.UserRequest,
                AffiliateRequest = adminReq
            };

            // Delegate to AdminCommands
            return await _affiliateCommands.CreateAffiliateAsync(session);
        }
    }

}
