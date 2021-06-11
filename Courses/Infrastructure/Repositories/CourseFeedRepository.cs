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
    public class CourseFeedRepository : ICourseFeedRepository
    {
        private const string CONNECTION_STRING_NAME = "S2SLMS";
        private const string GET_FEED_QUERY_NAME = "Queries:GetCourseFeed";
        private const string ADD_POST_QUERY_NAME = "Queries:AddPostToCourseFeed";
        private const string EDIT_POST_QUERY_NAME = "Queries:EditPostInCourseFeed";
        private const string DELETE_POST_QUERY_NAME = "Queries:DeletePostFromCourseFeed";

        private readonly IConfiguration _config;

        public CourseFeedRepository(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        public async Task<CourseFeedPost[]> GetCourseFeed(int course_id)
        {
            List<CourseFeedPostDTO> courseFeed = new List<CourseFeedPostDTO>();

            using (var connection = new SqlConnection(_config.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(
                    string.Format(_config.GetValue<string>(GET_FEED_QUERY_NAME), course_id)
                    , connection))
                {
                    var rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        courseFeed.Add(new CourseFeedPostDTO()
                        {
                            id = int.Parse(rdr["id"].ToString()),
                            course_id = int.Parse(rdr["course_id"].ToString()),
                            datetime = rdr.GetDateTime(rdr.GetOrdinal("dateandtime")),
                            content = rdr["content"].ToString()
                        });
                    }
                }
            }
            return courseFeed.Select(cfeed => cfeed.ToModel()).ToArray();
        }

        public async Task AddPost(CourseFeedPost cfpost)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(
                    string.Format(_config.GetValue<string>(ADD_POST_QUERY_NAME), cfpost.CourseId, cfpost.DateTime, cfpost.Content)
                    , connection))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EditPost(CourseFeedPost cfpost)
        {
            List<CourseFeedPostDTO> courseFeed = new List<CourseFeedPostDTO>();

            using (var connection = new SqlConnection(_config.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(
                    string.Format(_config.GetValue<string>(EDIT_POST_QUERY_NAME), cfpost.PostId, cfpost.Content)
                    , connection))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeletePost(CourseFeedPost cfpost)
        {
            List<CourseFeedPostDTO> courseFeed = new List<CourseFeedPostDTO>();

            using (var connection = new SqlConnection(_config.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(
                    string.Format(_config.GetValue<string>(DELETE_POST_QUERY_NAME), cfpost.PostId)
                    , connection))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
