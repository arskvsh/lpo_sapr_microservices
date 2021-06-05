using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S2S_LMS.Domain.Entities;
using S2S_LMS.Domain.Interfaces;

namespace S2S_LMS.Domain.Services
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
    }
}
