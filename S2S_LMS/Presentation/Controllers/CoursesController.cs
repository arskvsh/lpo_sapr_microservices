using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S2S_LMS.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using S2S_LMS.Domain.Interfaces;

namespace S2S_LMS.Presentation.Controllers
{
    [ApiController]
    [Route(template:"courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await _courseService.GetCourses()).Select(с => new CourseModel(с)));
        }
    }
}
