using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.DriverInterfaces;
using Application.Interfaces.RegisterHandler;
using Application.RoleRequeste;
using Domain.Enums;


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

        public async Task<Result<int>> RegisterAsync(CreateRoleSession  request)
        {
            if (request== null)
                return Result<int>.Failure("Invalid Driver request payload");
            var DriverRequest = request.RoleRequest as CreateDriverRequest;


            // Wrap into your existing CreateDriverSession
            var session = new CreatDriverSession
            {
                UserRequest = request.UserRequest,
                DriverRequest = DriverRequest
            };

            // Delegate to DriverCommands
            return await _DriverCommands.CreateDriverAsync(session);
        }
    }

}
