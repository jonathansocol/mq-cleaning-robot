using MQ.CleaningRobot.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.CleaningRobot.Models
{
    public class Navigator
    {
        private readonly string[][] _map;

        public Navigator(string[][] map)
        {
            _map = map;
        }

        public bool IsCollision(Coordinate position)
        {
            if (position.X < 0 || position.Y < 0)
            {
                return true;
            }

            if (position.X > _map.Length - 1 || position.Y > _map.Length - 1)
            {
                return true;
            }

            var cellContent = _map[position.Y][position.X];

            if (cellContent.ToLower() == "c" || cellContent == null)
            {
                return true;
            }

            return false;
        }

        public void SetVisited(Coordinate coordinates)
        {
            _map[coordinates.Y][coordinates.X] = "v";
        }

        public void SetClean(Coordinate coordinates)
        {
            _map[coordinates.Y][coordinates.X] = "l";
        }

        public NavigatorResults ExportResults()
        {
            var visitedCells = new List<Coordinate>();
            var cleanedCells = new List<Coordinate>();

            for (int i = 0; i < _map.Length; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    var cell = _map[i][j];

                    if (cell.ToLower() == "v")
                    {
                        visitedCells.Add(new Coordinate(j, i));
                    }
                    
                    if (cell.ToLower() == "l")
                    {
                        visitedCells.Add(new Coordinate(j, i));
                        cleanedCells.Add(new Coordinate(j, i));
                    }
                }
            }

            return new NavigatorResults(visitedCells, cleanedCells);
        }
    }
}
