using Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Infrastructure.DTO
{
    public class CourseFeedPostDTO
    {
        public int id { get; set; }
        public int course_id { get; set; }
        public DateTime datetime { get; set; }
        public string content { get; set; }

        public CourseFeedPost ToModel()
        {
            return new CourseFeedPost()
            {
                PostId = id,
                DateTime = datetime,
                Content = content
            };
        }
    }
}
