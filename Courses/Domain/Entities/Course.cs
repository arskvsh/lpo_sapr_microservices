using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher_Lect { get; set; }
        public string Teacher_Pract { get; set; }
        public string Teacher_Lab { get; set; }
        public string Description { get; set; }
        public string[] Feed { get; set; }
    }
}
