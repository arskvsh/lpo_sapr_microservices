using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S2S_LMS.Domain.Entities;

namespace S2S_LMS.Presentation.Models
{
    public class CourseModel
    {
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Description { get; set; }

        public CourseModel(Course course)
        {
            Name = course?.Name ?? throw new ArgumentNullException(nameof(course.Name));
            Teacher = course?.Teacher ?? throw new ArgumentNullException(nameof(course.Teacher));
            Description = course?.Description ?? throw new ArgumentNullException(nameof(course.Description));
        }
    }
}
