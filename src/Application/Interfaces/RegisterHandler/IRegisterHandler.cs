

using Application.Common.Models;
using Application.RoleRequeste;

namespace Application.Interfaces.RegisterHandler
{
   public interface IRegisterHandler
    {
        string RoleName { get; }
        Task<Result<int>> RegisterAsync(CreateRoleSession request);
    }
}
