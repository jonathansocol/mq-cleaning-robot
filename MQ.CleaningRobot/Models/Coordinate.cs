using System;

namespace MQ.CleaningRobot.Models
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;            
        }

        public int X { get; set; }

        public int Y { get; set; }
        
        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }


    }
}