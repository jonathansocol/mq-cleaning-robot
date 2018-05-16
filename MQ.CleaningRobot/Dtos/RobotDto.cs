using MQ.CleaningRobot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQ.CleaningRobot.Dtos
{
    public class RobotDto
    {
        public string[][] Map { get; set; }

        public string[] Commands { get; set; }

        public PositionDto Start { get; set; }

        public short Battery { get; set; }
    }
}
