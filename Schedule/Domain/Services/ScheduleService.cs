using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Domain.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleProvider _scheduleRepository;
        public ScheduleService(IScheduleProvider provider)
        {
            _scheduleRepository = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        public async Task<ESchedule> GetScheduleFromMISIS(ESchedule schedule)
        {
            return await _scheduleRepository.GetScheduleFromMISIS(schedule);
        }
    }
}
