using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;

namespace Courses.Domain.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository repository)
        {
            _courseRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Course[]> GetCourses()
        {
            return await _courseRepository.GetCourses();
        }

        public async Task<Course> GetCourse(int course_id)
        {
            return await _courseRepository.GetCourse(course_id);
        }
    }
}
