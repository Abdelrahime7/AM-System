

using Application.Assisstants.Dto_s.session;
using Application.Common.Models;

namespace Application.Interfaces.AssisstantInterfaces
{
    public interface IAssisstantQueries
    {
        public Task<Result<AssisstantSessionResponse>> GetById(int id);
        public Task<Result<IEnumerable<AssisstantSessionResponse>>> GetAllAssisstants();
    }
}
