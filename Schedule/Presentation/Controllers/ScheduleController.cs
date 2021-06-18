using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sentry;
using Schedule.Presentation.Models;
using Schedule.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Schedule.Presentation.Controllers
{
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly ILogger<ScheduleController> _logger;
        private IConfiguration _config;

        public ScheduleController(IConfiguration config, IScheduleService scheduleService, ILogger<ScheduleController> logger)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _scheduleService = scheduleService ?? throw new ArgumentNullException(nameof(scheduleService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("schedule")]
        [HttpPost]
        public async Task<IActionResult> GetScheduleFromMISIS([FromBody]ScheduleModel input)
        {
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Получен запрос на получение данных от третьей стороны");

                return Ok(new ScheduleModel(await _scheduleService.GetScheduleFromMISIS(input.ToEntity())));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка на третьей стороне");

                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        [Route("schedule/current")]
        [HttpPost]
        public async Task<IActionResult> GetCurrentScheduleFromMISIS([FromBody]ScheduleModel input)
        {
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry(_config.GetSection("Sentry").Value)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            try
            {
                logger.Information("Получен запрос на получение данных от третьей стороны");

                input.Start_Date = DateTime.Now;

                return Ok(new ScheduleModel(await _scheduleService.GetScheduleFromMISIS(input.ToEntity())));
                //return Ok((await _courseService.GetScheduleFromMISIS()).Select(s => new ScheduleModel(s)));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка на третьей стороне");

                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
    }
}
