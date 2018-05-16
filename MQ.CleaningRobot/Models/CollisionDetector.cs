using MQ.CleaningRobot.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.CleaningRobot.Models
{
    public class CollisionDetector
    {
        private readonly string[][] _map;

        public CollisionDetector(string[][] map)
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

        public bool IsVisited(Coordinate coordinates)
        {
            return _map[coordinates.Y][coordinates.X] == "v";
        }

        public void SetVisited(Coordinate coordinates)
        {
            _map[coordinates.Y][coordinates.X] = "v";
        }

        public bool IsClean(Coordinate coordinates)
        {
            return _map[coordinates.Y][coordinates.X] == "l";
        }

        public void SetClean(Coordinate coordinates)
        {
            _map[coordinates.Y][coordinates.X] = "l";
        }
    }
}
