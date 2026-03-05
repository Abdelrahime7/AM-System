using Application.CallsLog.DTOs;
using Application.Common.Models;
using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using Application.Interfaces.CallLogInterfaces;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.CallsLog.Features.Queries;

public partial class CallLogQueries(
    ICallLogRepository repository,
    IEntityMapper<CallLog, CreateCallLogRequest, UpdateCallLogRequest, CallLogrResponse> mapper)
    : ICallLogQueries
    {
        private readonly ICallLogRepository _repository = repository;
        private readonly IEntityMapper<CallLog, CreateCallLogRequest, UpdateCallLogRequest,
            CallLogrResponse> _mapper = mapper;

        public async Task<Result<CallLogrResponse>> GetCallLogByIDAsync(int id)
        {
            try
            {
                var callLog = await _repository.GetByIdAsync(id);
                if (callLog == null)
                    return Result<CallLogrResponse>.Failure("No call Log Found");

                var response = _mapper.ToResponse(callLog);
                return Result<CallLogrResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<CallLogrResponse>.Failure($"failed to fetch call Log: {ex.Message}");
            }
        }
    }
