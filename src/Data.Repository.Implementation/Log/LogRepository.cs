using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Configuration.Context;
using Data.Repository.Implementation.GenericRepository;
using Domain.Model;
using Domain.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementation
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        public LogRepository(LogDbContext context) : base(context)
        {
        }

        public async Task<List<Log>> GetLogsByDates(DateTime startDate, DateTime endDate) =>
            await _context.Set<Log>()
                .Where(a => startDate.Date >= a.Logged.Date
                            && endDate.Date <= a.Logged.Date)
        .ToListAsync();

        public async Task<List<Log>> GetLogByApplicationAndDate(int applicationId, DateTime logged) =>
            await _context.Set<Log>()
                .Where(a => a.ApplicationId == applicationId
                            && a.Logged.Date == logged)
                .ToListAsync();

        public async Task<List<Log>> GetLogByApplicationAndYearAndMonth(int applicationId, DateTime logged) =>
            await _context.Set<Log>()
                .Where(a => a.ApplicationId == applicationId
                            && a.Logged.Year == logged.Year
                            && a.Logged.Month == logged.Month)
                .ToListAsync();

        public async Task<List<Log>> GetLogByYearAndMonth(DateTime logged) =>
            await _context.Set<Log>()
                .Where(a => a.Logged.Year == logged.Year
                            && a.Logged.Month == logged.Month)
                .ToListAsync();
    }
}
