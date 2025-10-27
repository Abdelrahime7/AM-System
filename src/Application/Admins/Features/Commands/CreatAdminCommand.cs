using Application.Admins.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.AdminInterfaces;

namespace Application.Admins.Features.Commands
{
    partial class AdminCommands() : IAdminCommands
    {
        public Task<Result<int>> CreateAdminAsync(CreatAdminSession request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAdminAsnc(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> UpdateAdminAsnc(UpdateAdminSession request)
        {
            throw new NotImplementedException();
        }
    }
}
