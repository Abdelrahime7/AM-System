

using Application.Common.Models;
using Application.Admins.DTO_s.session;

namespace Application.Interfaces.AdminInterfaces
{
    public interface IAdminQueries
    {
        public Task<Result<AdminSessionResponse>> GetById(int id);
        public Task<Result<IEnumerable<AdminSessionResponse>>> GetAllAdmins();
    }
}
