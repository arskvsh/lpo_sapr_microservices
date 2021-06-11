using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Domain.Entities
{
    public class CourseFeedPost
    {
        public int PostId { get; set; }
        public int CourseId { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
    }
}
