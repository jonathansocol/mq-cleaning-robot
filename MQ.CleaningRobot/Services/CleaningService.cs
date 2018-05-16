using MQ.CleaningRobot.Dtos;
using MQ.CleaningRobot.Models;
using System.Collections.Generic;

namespace MQ.CleaningRobot.Services
{
    public class CleaningService
    {
        public CleaningPlanResultsDto ExecuteCleaningProcess(RobotDto robotConfiguration)
        {
            var position = new RobotPosition(robotConfiguration.Start.X, robotConfiguration.Start.Y, robotConfiguration.Start.Facing);
            var robot = new Robot(position, robotConfiguration.Battery);

            var instructions = new CleaningPlanInstructionsDto
            {
                Map = robotConfiguration.Map,
                Instructions = new Queue<string>(robotConfiguration.Commands)
            };

            return robot.ExecuteCleaningPlan(instructions);
        }
    }
}
