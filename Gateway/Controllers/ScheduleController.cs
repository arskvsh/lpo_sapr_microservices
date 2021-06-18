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
    public class ScheduleController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger<ScheduleController> _logger;

        public ScheduleController(IConfiguration config, ILogger<ScheduleController> logger)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("api/v1/schedule")]
        [HttpGet]
        public async Task<IActionResult> GetScheduleFromMisis(object value)
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
                    var uri = _config.GetSection("ScheduleUri").Value;
                    var resultMessage = await client.PostAsJsonAsync($"{uri}schedule", value);
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

        [Route("api/v1/schedule/current")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentScheduleFromMisis(object value)
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
                    var uri = _config.GetSection("ScheduleUri").Value;
                    var resultMessage = await client.PostAsJsonAsync($"{uri}schedule/current", value);
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
