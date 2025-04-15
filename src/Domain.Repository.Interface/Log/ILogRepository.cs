using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repository.Interface;

namespace Domain.Repository.Interface
{
    public interface ILogRepository : IGenericRepository<Log>
    {
        Task<List<Log>> GetLogsByDates(DateTime startDate, DateTime endDate);
        Task<List<Log>> GetLogByApplicationAndDate(int applicationId, DateTime logged);
        Task<List<Log>> GetLogByApplicationAndYearAndMonth(int applicationId, DateTime logged);
        Task<List<Log>> GetLogByYearAndMonth(DateTime logged);
    }
}
