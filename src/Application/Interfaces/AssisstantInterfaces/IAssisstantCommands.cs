

using Application.Assisstants.Dto_s.session;
using Application.Common.Models;


namespace Application.Interfaces.AssisstantInterfaces
{
    public interface IAssisstantCommands
    {
        public Task<Result<int>> CreateAssisstantAsync(CreatAssisstantSession request);
        public Task<Result<bool>> DeleteAssisstantAsnc(int Id);
        public Task<Result<bool>> UpdateAssisstantAsnc(UpdateAssisstantSession request);

    }
}
