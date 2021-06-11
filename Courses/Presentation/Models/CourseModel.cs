using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Presentation.Models
{
    public class CourseModel
    {
        public string Name { get; set; }
        public string TLect { get; set; }
        public string TPract { get; set; }
        public string TLab { get; set; }
        public string Description { get; set; }

        public CourseModel(Course course)
        {
            Name = course?.Name ?? throw new ArgumentNullException(nameof(course.Name));
            TLect = course?.Teacher_Lect ?? throw new ArgumentNullException(nameof(course.Teacher_Lect));
            TPract = course?.Teacher_Pract ?? throw new ArgumentNullException(nameof(course.Teacher_Pract));
            TLab = course?.Teacher_Lab ?? throw new ArgumentNullException(nameof(course.Teacher_Lab));
            Description = course?.Description ?? throw new ArgumentNullException(nameof(course.Description));
        }
    }
}
