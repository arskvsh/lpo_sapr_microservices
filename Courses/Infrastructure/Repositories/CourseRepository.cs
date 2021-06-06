using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Courses.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private const string CONNECTION_STRING_NAME = "S2SLMS";

        private readonly IConfiguration _config;

        public CourseRepository(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        public async Task<Course[]> GetCourses()
        {
            List<CourseDTO> courses = new List<CourseDTO>();

            using (var connection = new SqlConnection(_config.GetConnectionString(CONNECTION_STRING_NAME)))
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
