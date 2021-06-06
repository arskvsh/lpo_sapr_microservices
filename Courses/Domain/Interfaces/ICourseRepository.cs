using S2S_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S2S_LMS.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course[]> GetCourses();
    }
}
