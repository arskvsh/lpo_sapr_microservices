using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Text;
using Serilog.Core;

namespace Gateway.Controllers
{
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(IConfiguration config, ILogger<CoursesController> logger)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("api/v1/courses")]
        [HttpGet]
        public async Task<IActionResult> GetCoursesList()
        {
            Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
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
                logger.Fatal("Произошла фатальная ошибка на шлюзе");

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

        }

        [Route("api/v1/courses/{course_id}")]
        [HttpGet]
        public async Task<IActionResult> GetCourse([FromRoute]int course_id)
        {
            Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
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
                logger.Fatal("Произошла фатальная ошибка на шлюзе");

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

        }
        [Route("api/v1/courses/{course_id}/feed")]
        [HttpGet]
        public async Task<IActionResult> GetCourseFeed(int course_id)
        {
            Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Шлюз обрабатывает GET-запрос");

                using (HttpClient client = new HttpClient())
                {
                    var url = _config.GetSection("CoursesUri").Value;
                    var resultMessage = await client.GetAsync($"{url}courses/{course_id}/feed");
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.Fatal("Произошла фатальная ошибка на шлюзе");

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

        }

        [Route("api/v1/courses/{course_id}/feed/add")]
        [HttpPost]
        public async Task<IActionResult> AddPost(object value, int course_id)
        {
            Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Шлюз обрабатывает POST-запрос");

                using (HttpClient client = new HttpClient())
                {
                    var url = _config.GetSection("CoursesUri").Value;
                    var resultMessage = await client.PostAsJsonAsync($"{url}courses/{course_id}/feed/add", value);
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.Fatal("Произошла фатальная ошибка на шлюзе");

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

        }

        [Route("api/v1/courses/{course_id}/feed/{post_id}/edit")]
        [HttpPut]
        public async Task<IActionResult> EditPost([FromBody]object value, [FromRoute] int course_id, [FromRoute] int post_id)
        {
            Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Шлюз обрабатывает PUT-запрос");

                using (HttpClient client = new HttpClient())
                {
                    var url = _config.GetSection("CoursesUri").Value;
                    var resultMessage = await client.PutAsJsonAsync($"{url}courses/{course_id}/feed/{post_id}/edit", value);
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.Fatal("Произошла фатальная ошибка на шлюзе");

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [Route("api/v1/courses/{course_id}/feed/{post_id}/delete")]
        [HttpDelete]
        public async Task<IActionResult> DeletePost([FromRoute]int course_id, [FromRoute]int post_id)
        {
            Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Шлюз обрабатывает DELETE-запрос");

                using (HttpClient client = new HttpClient())
                {
                    var url = _config.GetSection("CoursesUri").Value;
                    var resultMessage = await client.DeleteAsync($"{url}courses/{course_id}/feed/{post_id}/delete");
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.Fatal("Произошла фатальная ошибка на шлюзе");

                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
