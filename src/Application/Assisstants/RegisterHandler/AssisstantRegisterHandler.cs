

using Application.Assisstants.Dto_s;
using Application.Assisstants.Dto_s.session;
using Application.Common.Models;
using Application.Interfaces.AssisstantInterfaces;
using Application.Interfaces.RegisterHandler;
using Application.RoleRequeste;
using Domain.Enums;
using System.Text.Json;

namespace Application.Assisstants.RegisterHandler
{
    public class AssisstantRegisterHandler : IRegisterHandler
    {
        private readonly IAssisstantCommands _AssisstantCommands;

        public string RoleName => UserRole.Assistant.ToString();

        public AssisstantRegisterHandler(IAssisstantCommands AssisstantCommands)
        {
            _AssisstantCommands = AssisstantCommands;
        }

        public async Task<Result<int>> RegisterAsync(CreateRoleSession request)
        {
            // Cast the role-specific payload
            var AssisstantReq = JsonSerializer.Deserialize<
                CreatAssisstantRequest>(request.RoleRequest.ToString());

            if (AssisstantReq == null)
                return Result<int>.Failure("Invalid Assisstant request payload");

            // Wrap into your existing CreateAssisstantSession
            var session = new CreatAssisstantSession
            {
                userRequest = request.UserRequest,
                assisstantRequest = AssisstantReq
            };

            // Delegate to AssisstantCommands
            return await _AssisstantCommands.CreateAssisstantAsync(session);
        }
    }

}
