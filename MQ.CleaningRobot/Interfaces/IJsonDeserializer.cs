using MQ.CleaningRobot.Models;

namespace MQ.CleaningRobot.Interfaces
{
    public interface IJsonDeserializer
    {
        RobotInput Deserialize(string json);
    }
}