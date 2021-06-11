using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Gateway.Controllers
{
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private IConfiguration _config;

        public CoursesController(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        [Route("api/v1/courses")]
        [HttpGet]
        public async Task<IActionResult> GetCoursesList()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://5669ac43d4bf4ea7ad072ba57496940b@o825521.ingest.sentry.io/5811140")
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Шлюз обрабатывает GET-запрос");

                using (HttpClient client = new HttpClient())
                {
                    var uri = _config.GetSection("CoursesUri").Value;
                    var resultMessage = await client.GetAsync($"{uri}courses");
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.Error("Произошла ошибка на шлюзе");

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

        }

        [Route("api/v1/courses/{course_id}")]
        [HttpGet]
        public async Task<IActionResult> GetCourse(int course_id)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://5669ac43d4bf4ea7ad072ba57496940b@o825521.ingest.sentry.io/5811140")
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Шлюз обрабатывает GET-запрос");

                using (HttpClient client = new HttpClient())
                {
                    var uri = _config.GetSection("CoursesUri").Value;
                    var resultMessage = await client.GetAsync($"{uri}courses/{course_id}");
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.Error("Произошла ошибка на шлюзе");

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

        }
    }
}
