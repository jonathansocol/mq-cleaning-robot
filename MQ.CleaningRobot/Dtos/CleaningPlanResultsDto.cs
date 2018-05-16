using MQ.CleaningRobot.Dtos;
using MQ.CleaningRobot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.CleaningRobot.Dtos
{
    public class CleaningPlanResultsDto
    {
        public List<Coordinate> Visited { get; set; }

        public List<Coordinate> Cleaned { get; set; }

        public Position Final { get; set; }

        public int Battery { get; set; }
    }
}
