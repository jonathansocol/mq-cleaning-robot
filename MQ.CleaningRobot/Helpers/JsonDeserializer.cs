using MQ.CleaningRobot.Interfaces;
using MQ.CleaningRobot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.CleaningRobot.Helpers
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public RobotInput Deserialize(string json)
        {
            if(string.IsNullOrEmpty(json))
            {
                throw new FormatException();
            }

            return JsonConvert.DeserializeObject<RobotInput>(json);
        }
    }
}
