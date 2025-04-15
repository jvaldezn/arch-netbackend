using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dto;
using Data.Util;

namespace Domain.UseCase
{
    public interface ILogUseCase
    {
        Task<Response<LogDto>> RegisterLog(LogDto information);
        Task<Response<IEnumerable<LogDto>>> GetLogsByDates(DateTime startDate, DateTime endDate);
        Task<Response<IEnumerable<LogDto>>> GetLogByApplicationAndDate(int applicationId, DateTime logged);
        Task<Response<IEnumerable<LogDto>>> GetLogByApplicationAndYearAndMonth(int applicationId, DateTime logged);
        Task<Response<IEnumerable<LogDto>>> GetLogByYearAndMonth(DateTime logged);
    }
}
