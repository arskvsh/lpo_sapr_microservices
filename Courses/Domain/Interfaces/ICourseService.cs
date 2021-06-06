using S2S_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S2S_LMS.Domain.Interfaces
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
    }
}
