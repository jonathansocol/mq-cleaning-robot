using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.CleaningRobot.Dtos
{
    public class CleaningPlanInstructionsDto
    {
        public string[][] Map { get; set; }

        public Queue<string> Instructions { get; set; }
    }
}
