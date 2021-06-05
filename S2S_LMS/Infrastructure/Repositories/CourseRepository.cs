using S2S_LMS.Domain.Entities;
using S2S_LMS.Domain.Interfaces;
using S2S_LMS.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace S2S_LMS.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public async Task<Course[]> GetCourses()
        {
            List<CourseDTO> courses = new List<CourseDTO>();

            using(var connection = new SqlConnection("Server=localhost;Database=s2slms;Integrated Security=true;"))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SELECT * FROM dbo.Courses", connection))
                {
                    var rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        courses.Add(new CourseDTO()
                        {
                            id = int.Parse(rdr["id"].ToString()),
                            teacher = rdr["teacher"].ToString(),
                            description = rdr["description"].ToString()
                        });
                    }
                }
            }
            return courses.Select(c => c.ToModel()).ToArray();
        }
    }
}
