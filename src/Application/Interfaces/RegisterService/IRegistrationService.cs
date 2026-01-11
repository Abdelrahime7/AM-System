

using Application.Common.Models;
using Application.RoleRequeste;

namespace Application.Interfaces.RegisterService
{
    public interface IRegistrationService
    {
        Task<Result<int>> RegisterAsync(CreateRoleSession request);
    }
}
