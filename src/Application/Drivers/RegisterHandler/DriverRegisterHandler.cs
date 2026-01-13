using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.DriverInterfaces;
using Application.Interfaces.RegisterHandler;
using Application.RoleRequeste;
using Domain.Enums;
using System.Text.Json;


namespace Application.Drivers.RegisterHandler
{
    public class DriverRegisterHandler : IRegisterHandler
    {
        private readonly IDriverCommands _DriverCommands;

        public string RoleName => UserRole.Driver.ToString();

        public DriverRegisterHandler(IDriverCommands DriverCommands)
        {
            _DriverCommands = DriverCommands;
        }

        public async Task<Result<int>> RegisterAsync(CreateRoleSession request)
        {
            // Cast the role-specific payload
            var DriverReq = JsonSerializer.Deserialize<CreateDriverRequest>(request.RoleRequest.ToString());
            if (DriverReq == null)
                return Result<int>.Failure("Invalid Driver request payload");

            // Wrap into your existing CreateDriverSession
            var session = new CreatDriverSession
            {
                UserRequest = request.UserRequest,
                DriverRequest = DriverReq
            };

            // Delegate to DriverCommands
            return await _DriverCommands.CreateDriverAsync(session);
        }
    }

}
