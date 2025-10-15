using Application.CallsLog.DTOs;
using Application.Common.Models;
using Application.Delivery.DTOs;
using Application.Interfaces.CallLogInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.CallsLog.Features.Commands;


    public partial class CallLogCommands(ICallLogRepository repository,
    IEntityMapper<CallLog, CreateCallLogRequest, UpdateCallLogRequest,
        CallLogrResponse> mapper) : ICallLogCommands


    {

        private readonly ICallLogRepository _repository = repository;
        private readonly IEntityMapper<CallLog, CreateCallLogRequest,
            UpdateCallLogRequest, CallLogrResponse> _mapper = mapper;
        public async Task<Result<int>> CreatCallLogAsync(CreateCallLogRequest request)
        {
            try
            {
                var callLog = _mapper.ToEntity(request);
                await _repository.AddAsync(callLog);
                return Result<int>.Success(callLog.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure($"Error creating callLog: {ex.Message}");
            }

        }




    }


