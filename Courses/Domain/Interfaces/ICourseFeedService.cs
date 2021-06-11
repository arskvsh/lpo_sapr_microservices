using Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Служба для работы с интерфейсами
    /// </summary>
    public interface ICourseFeedService
    {
        /// <summary>
        /// Вернуть ленту курса
        /// </summary>
        /// <returns></returns>
        Task<CourseFeedPost[]> GetCourseFeed(int course_id);
        Task AddPost(CourseFeedPost post);
        Task EditPost(CourseFeedPost post);
        Task DeletePost(CourseFeedPost post);
    }
}
