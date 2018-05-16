using MQ.CleaningRobot.Dtos;
using MQ.CleaningRobot.Enums;
using MQ.CleaningRobot.Exceptions;
using System.Collections.Generic;

namespace MQ.CleaningRobot.Models
{
    public class Robot
    {
        #region Private Members

        private readonly string[][] _backOffStrategy = new string[][]
        {
            new string[] {"TR", "A" },
            new string[] {"TL", "B", "TR", "A"},
            new string[] {"TL", "TL", "A"},
            new string[] {"TR", "B", "TR", "A"},
            new string[] {"TL", "TL", "A"}
        };

        private CollisionDetector _collisionDetector;

        #endregion

        public Robot(Position startingPosition, short initialBatteryLevel, string[][] backOffStrategy = null)
        {
            CurrentPosition = startingPosition;
            Battery = new Battery(initialBatteryLevel);

            if (backOffStrategy != null)
            {
                _backOffStrategy = backOffStrategy;
            }

            VisitedCells = new List<Coordinate>();
            CleanedCells = new List<Coordinate>();
        }

        #region Properties

        public Position CurrentPosition { get; set; }

        public Battery Battery { get; }

        private List<Coordinate> VisitedCells { get; set; }

        private List<Coordinate> CleanedCells { get; set; }

        #endregion

        public CleaningPlanResultsDto ExecuteCleaningPlan(string[][] map, Queue<string> instructions)
        {
            _collisionDetector = new CollisionDetector(map);

            var startingCoordinates = new Coordinate(CurrentPosition.X, CurrentPosition.Y);

            _collisionDetector.SetVisited(startingCoordinates);
            VisitedCells.Add(startingCoordinates);

            while (Battery.HasCapacity && instructions.Count > 0)
            {
                var instruction = instructions.Dequeue();

                try
                {
                    ExecuteInstruction(instruction);
                }
                catch (CollisionException)
                {
                    ExecuteBackOffStrategy();
                }
                catch (OutOfBatteryException)
                {
                    break;
                }
                catch (BackOffStrategyFailedException)
                {
                    break;
                }
            }

            var result = new CleaningPlanResultsDto
            {
                Visited = VisitedCells,
                Cleaned = CleanedCells,
                Final = CurrentPosition,
                Battery = Battery.Level
            };

            return result;
        }

        private void ExecuteInstruction(string instruction)
        {
            switch (instruction)
            {
                case "TL":
                    Battery.Drain(1);
                    Turn(TurnDirection.Left);                    
                    break;
                case "TR":
                    Battery.Drain(1);
                    Turn(TurnDirection.Right);
                    break;
                case "A":
                    Battery.Drain(2);
                    TryMove(MoveDirection.Forward);
                    break;
                case "B":
                    Battery.Drain(3);
                    TryMove(MoveDirection.Backward);
                    break;
                case "C":
                    Battery.Drain(5);
                    Clean();
                    break;
            }
        }

        private void Turn(TurnDirection direction)
        {
            CurrentPosition.SetOrientation(direction);
        }

        private void TryMove(MoveDirection direction)
        {
            var newPosition = CurrentPosition.GetPosition(direction);

            if (_collisionDetector.IsCollision(newPosition))
            {
                throw new CollisionException();
            }            

            CurrentPosition.SetPosition(newPosition.X, newPosition.Y);

            if (!_collisionDetector.IsVisited(newPosition))
            {
                _collisionDetector.SetVisited(newPosition);
                VisitedCells.Add(newPosition);
            }            
        }

        private void Clean()
        { 
            var coordinates = new Coordinate(CurrentPosition.X, CurrentPosition.Y);

            if (!_collisionDetector.IsClean(coordinates))
            {                
                CleanedCells.Add(coordinates);
                _collisionDetector.SetClean(coordinates);
            }            
        }

        private void ExecuteBackOffStrategy()
        {
            var index = 0;

            while (index <= _backOffStrategy.Length)
            {
                var instructions = new Queue<string>(_backOffStrategy[index]);
               
                while (Battery.HasCapacity && instructions.Count > 0)
                {
                    var instruction = instructions.Dequeue();

                    try
                    {
                        ExecuteInstruction(instruction);
                    }
                    catch (CollisionException)
                    {
                        index++;
                        break;
                    }
                    catch (OutOfBatteryException)
                    {
                        break;
                    }
                }                
            }

            if (index > _backOffStrategy.Length - 1)
            {
                throw new BackOffStrategyFailedException();
            }
        }
    }
}
