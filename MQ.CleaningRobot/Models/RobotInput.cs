using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQ.CleaningRobot.Models
{
    public class RobotInput
    {
        public string[][] Map { get; set; }

        public string[] Commands { get; set; }

        public RobotPosition Start { get; set; }

        [Range(0, 100)]
        public string Battery { get; set; }
    }
}
