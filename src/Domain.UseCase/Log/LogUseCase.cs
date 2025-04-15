using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Dto;
using Data.Util.Validation;
using Data.Util;
using Domain.Repository.Interface;
using Microsoft.Extensions.Logging;
using Data.Configuration.Context;
using Data.Util.Helper;
using Domain.Model;

namespace Domain.UseCase
{
    public class LogUseCase : ILogUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<LogDbContext> _unitOfWork;
        private readonly ILogRepository _logRepository;
        private readonly ILogger<LogUseCase> _logger;

        public LogUseCase(IMapper mapper,
            IUnitOfWork<LogDbContext> unitOfWork,
            ILogRepository logRepository,
            ILogger<LogUseCase> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logRepository = logRepository;
            _logger = logger;
        }

        public async Task<Response<LogDto>> RegisterLog(LogDto information)
        {
            var validator = await new LogDtoValidator().ValidateAsync(information);
            if (!validator.IsValid)
                return Response<LogDto>.Failure(validator.Errors.GetErrorMessage());

            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = _mapper.Map<Log>(information);
                await _logRepository.AddAsync(entity);

                await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                return Response<LogDto>.Success(_mapper.Map<LogDto>(entity));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(string.Format(Messages.UnexpectedError, ex.Message));
                return Response<LogDto>.Failure(string.Format(Messages.UnexpectedError, ex.Message));
            }
        }

        public async Task<Response<IEnumerable<LogDto>>> GetLogsByDates(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                return Response<IEnumerable<LogDto>>.Failure(Messages.DidntSendInformationForConsume);
            }

            var logs = await _logRepository.GetLogsByDates(startDate, endDate);

            if (!logs.Any())
            {
                return Response<IEnumerable<LogDto>>.Failure(Messages.DidNotFindAnyResults);
            }

            return Response<IEnumerable<LogDto>>.Success(_mapper.Map<IEnumerable<LogDto>>(logs));
        }

        public async Task<Response<IEnumerable<LogDto>>> GetLogByApplicationAndDate(int applicationId, DateTime logged)
        {
            if (applicationId == 0 || logged == DateTime.MinValue)
            {
                return Response<IEnumerable<LogDto>>.Failure(Messages.DidntSendInformationForConsume);
            }

            var logs = await _logRepository.GetLogByApplicationAndDate(applicationId, logged);

            if (!logs.Any())
            {
                return Response<IEnumerable<LogDto>>.Failure(Messages.DidNotFindAnyResults);
            }

            return Response<IEnumerable<LogDto>>.Success(_mapper.Map<IEnumerable<LogDto>>(logs));
        }

        public async Task<Response<IEnumerable<LogDto>>> GetLogByApplicationAndYearAndMonth(int applicationId, DateTime logged)
        {
            if (applicationId == 0 || logged == DateTime.MinValue)
            {
                return Response<IEnumerable<LogDto>>.Failure(Messages.DidntSendInformationForConsume);
            }

            var logs = await _logRepository.GetLogByApplicationAndYearAndMonth(applicationId, logged);

            if (!logs.Any())
            {
                return Response<IEnumerable<LogDto>>.Failure(Messages.DidNotFindAnyResults);
            }

            return Response<IEnumerable<LogDto>>.Success(_mapper.Map<IEnumerable<LogDto>>(logs));
        }

        public async Task<Response<IEnumerable<LogDto>>> GetLogByYearAndMonth(DateTime logged)
        {
            var logs = await _logRepository.GetLogByYearAndMonth(logged);

            if (!logs.Any())
            {
                return Response<IEnumerable<LogDto>>.Failure(Messages.DidNotFindAnyResults);
            }

            return Response<IEnumerable<LogDto>>.Success(_mapper.Map<IEnumerable<LogDto>>(logs));
        }
    }
}
