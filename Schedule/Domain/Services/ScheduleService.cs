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
        private readonly IScheduleRepository _scheduleRepository;
        public ScheduleService(IScheduleRepository repository)
        {
            _scheduleRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<ESchedule> GetScheduleFromMISIS(ESchedule schedule)
        {
            return await _scheduleRepository.GetScheduleFromMISIS(schedule);
        }
    }
}
