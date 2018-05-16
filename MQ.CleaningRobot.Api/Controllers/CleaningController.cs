using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MQ.CleaningRobot.Dtos;
using MQ.CleaningRobot.Models;
using Newtonsoft.Json;

namespace MQ.CleaningRobot.Api.Controllers
{
    [Route("api/[controller]")]
    public class CleaningController : Controller
    {
        [HttpPost]
        public ActionResult Post([FromBody]string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return BadRequest();
            }

            var robotInput = JsonConvert.DeserializeObject<RobotDto>(json);

            var position = new RobotPosition(robotInput.Start.X, robotInput.Start.Y, robotInput.Start.Facing);
            var robot = new Robot(position, robotInput.Battery);

            var instructions = new CleaningPlanInstructionsDto
            {
                Map = robotInput.Map,
                Instructions = new Queue<string>(robotInput.Commands)
            };

            var results = robot.ExecuteCleaningPlan(instructions);

            return Ok(results);
        }        
    }
}
