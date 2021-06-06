using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Presentation.Models
{
    public class FeedModel
    {
        public Course Course { get; set; }
        public FeedPost Feed { get; set; }

        public FeedModel(Course course)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
            //Feed = course?.Feed ?? throw new ArgumentNullException(nameof(course.Feed));
        }
    }
}
