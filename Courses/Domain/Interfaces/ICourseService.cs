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
    public interface ICourseService
    {
        /// <summary>
        /// Возвращает список курсов
        /// </summary>
        /// <returns></returns>
        Task<Course[]> GetCourses();
        Task<Course> GetCourse(int course_id);
    }
}
