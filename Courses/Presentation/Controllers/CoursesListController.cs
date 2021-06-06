using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Courses.Domain.Interfaces;

namespace Courses.Presentation.Controllers
{
    [ApiController]
    [Route(template:"courses")]
    public class CoursesListController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesListController(ICourseService courseService)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await _courseService.GetCourses()).Select(с => new CourseListModel(с)));
        }
    }
}
