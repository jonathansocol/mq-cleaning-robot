using MQ.CleaningRobot.Enums;
using System;

namespace MQ.CleaningRobot.Models
{
    public class RobotPosition
    {
        private readonly char[] orientations = { 'W', 'N', 'E', 'S' };

        public RobotPosition(int x, int y, char facing)
        {
            X = x;
            Y = y;
            Facing = facing;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public char Facing { get; private set; }

        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetDirection(char direction)
        {
            Facing = direction;
        }

        public void SetOrientation(TurnDirection direction)
        {
            var index = Array.IndexOf(orientations, Facing);

            switch (direction)
            {
                case TurnDirection.Left:
                    Facing = index == 0 ?
                        orientations[orientations.Length - 1] : orientations[index - 1];
                    break;
                case TurnDirection.Right:
                    Facing = index == orientations.Length - 1 ?
                        orientations[0] : orientations[index + 1];
                    break;
            }
        }

        public Coordinate GetPosition(MoveDirection direction)
        {
            Coordinate newPosition = new Coordinate(X, Y);

            sbyte index1 = 0;
            sbyte index2 = 0;

            switch (direction)
            {
                case MoveDirection.Forward:
                    index1 = 1;
                    index2 = -1;
                    break;
                case MoveDirection.Backward:
                    index1 = -1;
                    index2 = 1;
                    break;
            }

            switch (Facing)
            {
                case 'W':
                    newPosition.X += index2;
                    break;
                case 'N':
                    newPosition.Y += index2;
                    break;
                case 'E':
                    newPosition.X += index1;
                    break;
                case 'S':
                    newPosition.Y += index1;
                    break;
            }

            return newPosition;
        }
    }    
}