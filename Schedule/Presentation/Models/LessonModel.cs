using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schedule.Domain.Entities;

namespace Schedule.Presentation.Models
{
    public class ScheduleModel
    {
        public int Branch;
        public int Group;
        public DateTime Start_Date;
        public List<Lesson> Lessons;
        public ScheduleModel()
        {

        }
        public ScheduleModel(ESchedule _schedule)
        {
            Branch = _schedule?.Branch ?? throw new ArgumentNullException(nameof(_schedule.Branch));
            Group = _schedule?.Group ?? throw new ArgumentNullException(nameof(_schedule.Group));
            Start_Date = _schedule?.StartDate ?? throw new ArgumentNullException(nameof(_schedule.StartDate));
            Lessons = _schedule?.Lessons ?? throw new ArgumentNullException(nameof(_schedule.Lessons));
        }
        public ESchedule ToEntity()
        {
            return new ESchedule
            {
                Branch = Branch,
                Group = Group,
                StartDate = Start_Date,
                Lessons = Lessons
            };
        }
    }
}
