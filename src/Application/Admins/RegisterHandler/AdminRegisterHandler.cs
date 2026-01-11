using Application.Admins.Dto_s;
using Application.Admins.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.AdminInterfaces;
using Application.Interfaces.RegisterHandler;
using Application.RoleRequeste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Admins.RegisterHandler
{
    public class AdminRegisterHandler : IRegisterHandler
    {
        private readonly IAdminCommands _adminCommands;

        public string RoleName => "Admin";

        public AdminRegisterHandler(IAdminCommands adminCommands)
        {
            _adminCommands = adminCommands;
        }

        public async Task<Result<int>> RegisterAsync(CreateRoleSession request)
        {
            // Cast the role-specific payload
            var adminReq = request.RoleRequest as CreateAdminRequest;
            if (adminReq == null)
                return Result<int>.Failure("Invalid Admin request payload");

            // Wrap into your existing CreateAdminSession
            var session = new CreatAdminSession
            {
                UserRequest = request.UserRequest,
                AdminRequest = adminReq
            };

            // Delegate to AdminCommands
            return await _adminCommands.CreateAdminAsync(session);
        }
    }

}
