using Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course[]> GetCourses();
    }
}
