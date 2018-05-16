using MQ.CleaningRobot.Dtos;
using Newtonsoft.Json;
using System;

namespace MQ.CleaningRobot.Client.Helpers
{
    public class JsonDeserializer
    {
        public RobotDto Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new FormatException();
            }

            return JsonConvert.DeserializeObject<RobotDto>(json);
        }
    }
}
