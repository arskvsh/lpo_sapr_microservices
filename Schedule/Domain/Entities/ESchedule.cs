using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Schedule.Domain.Entities
{
    public class ESchedule
    {
        public int Branch;
        public int Group;
        public DateTime StartDate;
        public List<Lesson> Lessons;
    }

    public class Lesson
    {
        public DateTime Day { get; set; }
        public int Number { get; set; }
        public string CourseName { get; set; }
        public string Type { get; set; }
        public List<string> TeacherNames { get; set; }
        public List<string> RoomNames { get; set; }
    }
}
