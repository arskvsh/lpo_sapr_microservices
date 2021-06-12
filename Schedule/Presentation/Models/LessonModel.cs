using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Presentation.Models
{
    public class ScheduleModel
    {
        public LessonModel[,] lessonModels;
        public ScheduleModel(LessonModel[,] _lessonModels)
        {
            lessonModels = _lessonModels;
        }
    }

    public class LessonModel
    {
        public DateTime Day { get; set; }
        public int Number { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public string RoomName { get; set; }
    }
}
