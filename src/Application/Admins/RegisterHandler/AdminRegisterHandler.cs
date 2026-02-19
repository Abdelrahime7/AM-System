using Application.Admins.Dto_s;
using Application.Admins.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.AdminInterfaces;
using Application.Interfaces.RegisterHandler;
using Application.RoleRequeste;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace Application.Admins.RegisterHandler
{
    public class AdminRegisterHandler : IRegisterHandler
    {
        private readonly IAdminCommands _adminCommands;

        public string RoleName => UserRole.Admin.ToString();

        public AdminRegisterHandler(IAdminCommands adminCommands)
        {
            _adminCommands = adminCommands;
        }

        public async Task<Result<int>> RegisterAsync(CreateRoleSession request)
        {

            var AdminRequest = request.RoleRequest as CreateAdminRequest;


            if (request == null)
                return Result<int>.Failure("Invalid Admin request payload");

            // Wrap into your existing CreateAdminSession
            var session = new CreatAdminSession
            {
                UserRequest = request.UserRequest,
                AdminRequest = AdminRequest
            };

            // Delegate to AdminCommands
            return await _adminCommands.CreateAdminAsync(session);
        }

      
    }
    public class SuperAdminRegisterHandler : IRegisterHandler
    {
        private readonly IAdminCommands _adminCommands;

        public string RoleName => UserRole.SuperAdmin.ToString();

        public SuperAdminRegisterHandler(IAdminCommands adminCommands)
        {
            _adminCommands = adminCommands;
        }

        public async Task<Result<int>> RegisterAsync(CreateRoleSession request)
        {

            var AdminRequest = request.RoleRequest as CreateAdminRequest;


            if (request == null)
                return Result<int>.Failure("Invalid Admin request payload");

            // Wrap into your existing CreateAdminSession
            var session = new CreatAdminSession
            {
                UserRequest = request.UserRequest,
                AdminRequest = AdminRequest
            };

            // Delegate to AdminCommands
            return await _adminCommands.CreateAdminAsync(session);
        }


    }

}
