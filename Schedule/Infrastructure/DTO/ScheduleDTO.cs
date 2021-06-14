using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.DTO
{
    public class ScheduleDTO
    {
        public int branch;
        public int group;
        public DateTime startdate;
        public List<LessonDTO> lessons;

        public ESchedule ToEntity()
        {
            return new ESchedule()
            {
                Branch = branch,
                Group = group,
                StartDate = startdate,
                Lessons = lessons.Select(l => l.ToEntity()).ToList()
            };
        }
    }
    public class LessonDTO
    {
        public DateTime day { get; set; }
        public int number { get; set; }
        public string courseName { get; set; }
        public string type { get; set; }
        public List<string> teacherNames { get; set; }
        public List<string> roomNames { get; set; }

        public Lesson ToEntity()
        {
            return new Lesson()
            {
                Day = day,
                Number = number,
                CourseName = courseName,
                Type = type,
                TeacherNames = teacherNames,
                RoomNames = roomNames
            };
        }
    }
}
