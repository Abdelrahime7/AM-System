using Application.Common.Models;
using Application.Users.DTOs;

namespace Application.Admins.Features.Commands
{
    partial class AdminCommands
    {
        public  async Task<Result<bool>> ChangeUserStatusAsync(ChangeStatusRequest request)
        {
            return await _userCommands.ChangeUserStatusAsync(request);
        }
    }
}
