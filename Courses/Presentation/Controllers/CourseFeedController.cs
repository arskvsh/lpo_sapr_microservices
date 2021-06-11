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

namespace Courses.Presentation.Controllers
{
    [ApiController]
    public class CourseFeedController : ControllerBase
    {
        private readonly ICourseFeedService _courseFeedService;
        private readonly ILogger<CoursesListController> _logger;

        public CourseFeedController(ICourseFeedService courseFeedService, ILogger<CoursesListController> logger)
        {
            _courseFeedService = courseFeedService ?? throw new ArgumentNullException(nameof(courseFeedService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //просмотр ленты курса
        [Route("courses/{course_id}/feed")]
        [HttpGet]
        public async Task<IActionResult> Get(int course_id)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://5669ac43d4bf4ea7ad072ba57496940b@o825521.ingest.sentry.io/5811140")
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Запрос на получение ленты курса");

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
        public async Task<IActionResult> AddPost([FromBody]CourseFeedPostModel model, int course_id)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://5669ac43d4bf4ea7ad072ba57496940b@o825521.ingest.sentry.io/5811140")
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                if (model == null)
                    return BadRequest("Запись не может быть пустой");

                model.CourseId = course_id;
                model.DateTime = DateTime.Now;

                return Ok((_courseFeedService.AddPost(model.ToEntity())));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        //редактирование содержимого поста
        [Route("courses/{course_id}/feed/edit")]
        [HttpPost]
        public async Task<IActionResult> EditPost([FromBody] CourseFeedPostModel model)
        {
            var logger = new LoggerConfiguration()
            .WriteTo.Sentry("https://5669ac43d4bf4ea7ad072ba57496940b@o825521.ingest.sentry.io/5811140")
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .CreateLogger();
            try
            {
                if (model == null)
                    return BadRequest("Текст записи не может быть пустой");

                return Ok((_courseFeedService.EditPost(model.ToEntity())));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        //удаление поста из ленты
        [Route("courses/{course_id}/feed/delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost([FromBody] CourseFeedPostModel model)
        {
            var logger = new LoggerConfiguration()
            .WriteTo.Sentry("https://5669ac43d4bf4ea7ad072ba57496940b@o825521.ingest.sentry.io/5811140")
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .CreateLogger();
            try
            {
                if (model == null)
                    return BadRequest("Текст записи не может быть пустой");

                return Ok((_courseFeedService.DeletePost(model.ToEntity())));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
    }
}
