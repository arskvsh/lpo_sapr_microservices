using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Presentation.Models
{
    public class CourseFeedPostModel
    {
        public int PostId { get; set; }
        public int CourseId { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }

        public CourseFeedPostModel()
        {
        }

        public CourseFeedPostModel(CourseFeedPost courseFeedPost)
        {
            PostId = courseFeedPost?.PostId ?? throw new ArgumentNullException(nameof(courseFeedPost.PostId));
            CourseId = courseFeedPost?.CourseId ?? throw new ArgumentNullException(nameof(courseFeedPost.CourseId));
            DateTime = courseFeedPost?.DateTime ?? throw new ArgumentNullException(nameof(courseFeedPost.DateTime));
            Content = courseFeedPost?.Content ?? throw new ArgumentNullException(nameof(courseFeedPost.Content));
        }

        public CourseFeedPost ToEntity()
        {
            return new CourseFeedPost()
            {
                PostId = this.PostId,
                CourseId = this.CourseId,
                DateTime = this.DateTime,
                Content = this.Content
            };
        }
    }
}
