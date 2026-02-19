using Application.Common.Models;
using Application.Interfaces.RegisterHandler;
using Application.Interfaces.RegisterService;
using Application.RoleRequeste;


namespace Infrastructure.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IDictionary<string, IRegisterHandler> _handlers;

        public RegistrationService(IEnumerable<IRegisterHandler> handlers)
        {
            _handlers = handlers.ToDictionary(h => h.RoleName);
        }

        public async Task<Result<int>> RegisterAsync(CreateRoleSession request)
        {
            var role = request.UserRequest.Role;

            if (_handlers.TryGetValue(role.ToString(), out var handler))
            {
                return await handler.RegisterAsync(request);
            }

            return Result<int>.Failure($"No handler found for role {role}");
        }
    }

}
