using MQ.CleaningRobot.Dtos;
using Newtonsoft.Json;
using System;

namespace MQ.CleaningRobot.Client.Helpers
{
    public static class JsonSerializer
    {
        public static RobotDto Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new FormatException();
            }

            return JsonConvert.DeserializeObject<RobotDto>(json);
        }

        public static string Serialize(CleaningPlanResultsDto results)
        {
            return JsonConvert.SerializeObject(results);
        }
    }
}
