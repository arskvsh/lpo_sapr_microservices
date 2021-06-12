using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sentry;
using Schedule.Presentation.Models;

namespace Schedule.Presentation.Controllers
{
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _courseService;

        [Route("schedule")]
        [HttpGet]
        public async Task<IActionResult> GetScheduleFromMISIS()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://5669ac43d4bf4ea7ad072ba57496940b@o825521.ingest.sentry.io/5811140")
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Получен запрос на получение данных от третьей стороны");

                return Ok((await _courseService.GetCourses()).Select(с => new CourseModel(с)));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка на третьей стороне");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
    }
}
