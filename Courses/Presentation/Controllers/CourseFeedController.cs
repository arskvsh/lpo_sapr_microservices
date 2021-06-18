using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Interfaces;
using Courses.Presentation.Models;
using Microsoft.Extensions.Logging;
using Serilog;
using Courses.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Courses.Presentation.Controllers
{
    [ApiController]
    public class CourseFeedController : ControllerBase
    {
        private readonly ICourseFeedService _courseFeedService;
        private IConfiguration _config;
        private readonly ILogger<CourseController> _logger;

        public CourseFeedController(IConfiguration config, ICourseFeedService courseFeedService, ILogger<CourseController> logger)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _courseFeedService = courseFeedService ?? throw new ArgumentNullException(nameof(courseFeedService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //просмотр ленты курса
        [Route("courses/{course_id}/feed")]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute]int course_id)
        {
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Получен запрос на получение ленты курса");

                return Ok((await _courseFeedService.GetCourseFeed(course_id)).Select(f => new CourseFeedPostModel(f)));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        //добавление поста в ленту курса
        [Route("courses/{course_id}/feed/add")]
        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody]CourseFeedPostModel model, [FromRoute]int course_id)
        {
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                if (model == null)
                    return BadRequest("Запись не может быть пустой");

                logger.Information("Получен запрос на добавление поста");

                model.CourseId = course_id;

                await _courseFeedService.AddPost(model.ToEntity());
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        //редактирование содержимого поста
        [Route("courses/{course_id}/feed/{post_id}/edit")]
        [HttpPut]
        public async Task<IActionResult> EditPost([FromBody]CourseFeedPostModel model, [FromRoute]int course_id, [FromRoute]int post_id)
        {
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                if (model == null || course_id <= 0 || post_id <= 0)
                    return BadRequest("Неверный формат запроса!");

                logger.Information("Получен запрос на редактирование поста");

                model.PostId = post_id;
                model.CourseId = course_id;

                await _courseFeedService.EditPost(model.ToEntity());
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        //удаление поста из ленты
        [Route("courses/{course_id}/feed/{post_id}/delete")]
        [HttpDelete]
        public async Task<IActionResult> DeletePost([FromRoute]int course_id, [FromRoute]int post_id)
        {
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                if (course_id <= 0 || post_id <= 0)
                    return BadRequest("Неверный формат запроса!");

                logger.Information("Получен запрос на удаление поста");

                await _courseFeedService.DeletePost(course_id, post_id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
    }
}
