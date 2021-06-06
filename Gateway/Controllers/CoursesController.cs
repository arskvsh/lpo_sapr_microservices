using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Gateway.Controllers
{
    [Route("api/v1/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private IConfiguration _config;

        public CoursesController(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        [HttpGet]
        public async Task<IActionResult> GetCoursesList()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = _config.GetSection("CoursesUrl").Value;
                    var resultMessage = await client.GetAsync($"{url}courses");
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

        }
    }
}
