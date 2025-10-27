

using Application.Common.Models;
using Application.Admins.DTO_s.session;

namespace Application.Interfaces.AdminInterfaces
{
    public interface IAdminCommands
    {
        public Task<Result<int>> CreateAdminAsync(CreatAdminSession request);
        public Task<Result<bool>> DeleteAdminAsnc(int Id);
        public Task<Result<bool>> UpdateAdminAsnc(UpdateAdminSession request);

    }
}
