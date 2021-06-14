using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;

namespace Courses.Domain.Services
{
    public class CourseFeedService : ICourseFeedService
    {
        private readonly ICourseFeedRepository _courseFeedRepository;
        public CourseFeedService(ICourseFeedRepository repository)
        {
            _courseFeedRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CourseFeedPost[]> GetCourseFeed(int course_id)
        {
            return await _courseFeedRepository.GetCourseFeed(course_id);
        }
        public async Task AddPost(CourseFeedPost post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            await _courseFeedRepository.AddPost(post);
        }
        public async Task EditPost(CourseFeedPost post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            await _courseFeedRepository.EditPost(post);
        }
        public async Task DeletePost(CourseFeedPost post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            await _courseFeedRepository.DeletePost(post);
        }
    }
}
