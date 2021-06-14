using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Schedule.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Schedule.Infrastructure.DTO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using Schedule.Domain.Interfaces;
using System.Globalization;

namespace Schedule.Infrastructure.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private const string GET_SCHEDULE_LINK = "GetLinks:Misis";

        private readonly IConfiguration _config;
        public ScheduleRepository(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<ESchedule> GetScheduleFromMISIS(ESchedule schedule)
        {
            ScheduleDTO scheduleDTO = new ScheduleDTO()
            {
                branch = schedule.Branch,
                group = schedule.Group,
                startdate = schedule.StartDate
            };

            MisisRequest misisReq = new MisisRequest
            {
                filial = schedule.Branch,
                group = schedule.Group.ToString(),
                start_date = schedule.StartDate.ToString("yyyy-MM-dd"),
                end_date = null,
                room = null,
                teacher = null
            };

            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent(misisReq.jsonRequest(), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_config.GetValue<string>(GET_SCHEDULE_LINK), UriKind.Absolute)
                };
                var resultMessage = await client.SendAsync(request);
                var result = await resultMessage.Content.ReadAsStringAsync();

                scheduleDTO.lessons = JsonToLessons(result);
            }

            return scheduleDTO.ToEntity();
        }

        private List<LessonDTO> JsonToLessons(string json)
        {
            List<LessonDTO> _lessons = new List<LessonDTO>();
            JObject jsonObject = JObject.Parse(json);

            for (int i = 1; i < 7; i++)
            {
                for (int j = 1; j < 7; j++)
                {
                    string lesson_index = $"$.schedule.bell_{j}.day_{i}.lessons[0]";
                    JToken lesson = jsonObject.SelectToken(lesson_index);
                    if (lesson != null && lesson.HasValues)
                    {
                        LessonDTO lsn = new LessonDTO
                        {
                            day = DateTime.Parse((string)jsonObject.SelectToken($"schedule_header.day_{i}.date" ?? ""), new CultureInfo("en-GB", true)),
                            number = j,
                            type = (string)jsonObject.SelectToken(lesson_index + ".type") ?? "",
                            courseName = (string)jsonObject.SelectToken(lesson_index + ".subject_name") ?? "",
                            teacherNames = jsonObject.SelectTokens(lesson_index+ ".teachers[*].name").Select(t => (string)t).ToList() ?? new List<string>(0),
                            roomNames = jsonObject.SelectTokens(lesson_index + ".rooms[*].name").Select(r => (string)r).ToList() ?? new List<string>(0)
                        };
                        _lessons.Add(lsn);
                    }
                }
            }
            return _lessons;
    }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class MisisRequest
    {
        public int filial;
        public string group;
        public int? room;
        public int? teacher;
        public string start_date;
        public string end_date;

        public string jsonRequest()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
