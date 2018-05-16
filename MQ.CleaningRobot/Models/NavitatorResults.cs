using MQ.CleaningRobot.Dtos;
using MQ.CleaningRobot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.CleaningRobot.Models
{
    public class NavigatorResults
    {
        public NavigatorResults(List<Coordinate> visitedCells, List<Coordinate> cleanedCells)
        {
            Visited = visitedCells;
            Cleaned = cleanedCells;
        }

        public List<Coordinate> Visited { get; }

        public List<Coordinate> Cleaned { get; }
    }
}
