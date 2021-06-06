using Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Infrastructure.DTO
{
    public class CourseDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string t_lect { get; set; }
        public string t_pract { get; set; }
        public string t_lab { get; set; }
        public string description { get; set; }

        public Course ToModel()
        {
            return new Course()
            {
                Id = id,
                Name = name, 
                Teacher_Lect = t_lect,
                Teacher_Pract = t_pract,
                Teacher_Lab = t_lab,
                Description = description
            };
        }
    }
}
