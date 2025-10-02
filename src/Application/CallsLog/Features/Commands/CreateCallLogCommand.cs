using Application.CallsLog.DTOs;
using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.CallLogInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.CallsLog.Features.Commands;

public partial class CallLogCommands : ICallLogCommands
{
    public Task<Result<int>> CreatCallLogAsync(CreateCallLogRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteCallLogAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateCallLogAsync(UpdateCallLogRequest request)
    {
        throw new NotImplementedException();
    }
}
