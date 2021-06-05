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
                using (var cmd = new SqlCommand(
                    "SELECT * from CoursesList"
                    , connection))
                {
                    var rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        courses.Add(new CourseDTO()
                        {
                            id = int.Parse(rdr["id"].ToString()),
                            name = rdr["name"].ToString(),
                            t_lect = rdr["t_lect_name"].ToString(),
                            t_pract = rdr["t_pract_name"].ToString(),
                            t_lab = rdr["t_lab_name"].ToString(),
                            description = rdr["description"].ToString()
                        });
                    }
                }
            }
            return courses.Select(c => c.ToModel()).ToArray();
        }
    }
}
