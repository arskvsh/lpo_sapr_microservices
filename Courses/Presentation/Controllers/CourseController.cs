using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Courses.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Sentry;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Courses.Presentation.Controllers
{
    [ApiController]
    
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;
        private IConfiguration _config;

        public CourseController(IConfiguration config, ICourseService courseService, ILogger<CourseController> logger)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("courses")]
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Запрос на получение списка курсов");

                return Ok((await _courseService.GetCourses()).Select(с => new CourseModel(с)));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        [Route("courses/{course_id}")]
        [HttpGet]
        public async Task<IActionResult> GetCourse(int course_id)
        {
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Запрос на получение информации об одном курсе");

                return Ok(new CourseModel((await _courseService.GetCourse(course_id))));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
    }
}
