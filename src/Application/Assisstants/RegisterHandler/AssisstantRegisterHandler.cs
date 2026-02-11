

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

            
            if (request == null)
                return Result<int>.Failure("Invalid Assisstant request payload");
           
            var assistantReq = request.RoleRequest as CreatAssisstantRequest;
 
            // Wrap into your existing CreateAssisstantSession
            var session = new CreatAssisstantSession
            {
                userRequest = request.UserRequest,

               
                assisstantRequest = assistantReq
            };

            // Delegate to AssisstantCommands
            return await _AssisstantCommands.CreateAssisstantAsync(session);
        }
    }

}
