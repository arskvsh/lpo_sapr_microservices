using Schedule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Domain.Interfaces
{
    public interface IScheduleProvider
    {
        public Task<ESchedule> GetScheduleFromMISIS(ESchedule schedule);
    }
}
