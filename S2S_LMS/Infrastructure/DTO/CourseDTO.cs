using S2S_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S2S_LMS.Infrastructure.DTO
{
    public class CourseDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string teacher { get; set; }
        public string description { get; set; }

        public Course ToModel()
        {
            return new Course() 
            { 
                Name = name, 
                Teacher = teacher, 
                Description = description 
            };
        }
    }
}
