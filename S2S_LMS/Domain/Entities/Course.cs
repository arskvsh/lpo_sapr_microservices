using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S2S_LMS.Domain.Entities
{
    public class Course
    {
        public string Name { get; set; }
        public string Teacher_Lect { get; set; }
        public string Teacher_Pract { get; set; }
        public string Teacher_Lab { get; set; }
        public string Description { get; set; }
    }
}
