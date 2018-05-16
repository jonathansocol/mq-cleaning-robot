using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MQ.CleaningRobot.Dtos;
using MQ.CleaningRobot.Models;
using MQ.CleaningRobot.Services;
using Newtonsoft.Json;

namespace MQ.CleaningRobot.Api.Controllers
{
    [Route("api/[controller]")]
    public class CleaningController : Controller
    {
        private readonly CleaningService _cleaningService;

        public CleaningController(CleaningService cleaningService)
        {
            _cleaningService = cleaningService;
        }

        [HttpPost]
        public ActionResult Post([FromBody]RobotDto robotInput)
        {
            var results = _cleaningService.ExecuteCleaningProcess(robotInput);

            return Ok(results);
        }        
    }
}
