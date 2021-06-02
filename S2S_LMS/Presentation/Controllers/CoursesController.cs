using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S2S_LMS.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace S2S_LMS.Presentation.Controllers
{
    [ApiController]
    [Route(template:"courses")]
    public class CoursesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new CourseModel[]
            {
                new CourseModel(){ Name="Компьютерная графика в САПР",Teacher="Аристов А.О.",Description="Есть курсач, по всем темам коллоквиумы, оценка ставится автоматом" },
                new CourseModel(){ Name="ЛПО САПР",Teacher="Зорин И.А.",Description="Есть неофициальный курсач, автомат, если сделать межсервисную авторизацию" },
            });
        }
    }
}
