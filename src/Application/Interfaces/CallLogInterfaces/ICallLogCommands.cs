

using Application.Common.Models;
using Application.CallsLog.DTOs;

namespace Application.Interfaces.CallLogInterfaces
{
    public interface ICallLogCommands
    {
        Task<Result<int>> CreatCallLogAsync(CreateCallLogRequest request);
        Task<Result<bool>> DeleteCallLogAsync(int ID);
        Task<Result<bool>> UpdateCallLogAsync(UpdateCallLogRequest request);
      

    }
}
