using MQ.CleaningRobot.Dtos;
using MQ.CleaningRobot.Enums;
using MQ.CleaningRobot.Exceptions;
using System;
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

        private Navigator _navigator;

        #endregion

        public Robot(RobotPosition startingPosition, short initialBatteryLevel, string[][] backOffStrategy = null)
        {
            CurrentPosition = startingPosition;
            Battery = new Battery(initialBatteryLevel);

            if (backOffStrategy != null)
            {
                _backOffStrategy = backOffStrategy;
            }
        }

        #region Properties

        public RobotPosition CurrentPosition { get; set; }

        public Battery Battery { get; }

        #endregion

        public CleaningPlanResultsDto ExecuteCleaningPlan(CleaningPlanInstructionsDto cleanPlanInstructions)
        {
            _navigator = new Navigator(cleanPlanInstructions.Map);

            var instructions = cleanPlanInstructions.Instructions;
            var startingCoordinates = new Coordinate(CurrentPosition.X, CurrentPosition.Y);

            _navigator.SetVisited(startingCoordinates);

            while (Battery.HasCapacity && instructions.Count > 0)
            {
                var instruction = instructions.Dequeue();

                try
                {
                    ExecuteInstruction(instruction);
                }
                catch (Exception ex)
                {
                    if (ex is CollisionException || ex is OutOfBatteryException)
                    {
                        try
                        {
                            ExecuteBackOffStrategy();
                        }
                        catch (Exception e)
                        {
                            if (e is BackOffStrategyFailedException || e is OutOfBatteryException)
                            {
                                break;
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    else
                    {
                        throw;
                    }                    
                }
            }

            var navigatorResults = _navigator.ExportResults();

            var result = new CleaningPlanResultsDto
            {
                Visited = navigatorResults.Visited,
                Cleaned = navigatorResults.Cleaned,
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

            if (_navigator.IsCollision(newPosition))
            {
                throw new CollisionException();
            }

            CurrentPosition.SetPosition(newPosition.X, newPosition.Y);

             _navigator.SetVisited(newPosition);            
        }

        private void Clean()
        {
            var coordinates = new Coordinate(CurrentPosition.X, CurrentPosition.Y);

            _navigator.SetClean(coordinates);            
        }

        private void ExecuteBackOffStrategy(int index = 0)
        {
            if (index >= _backOffStrategy.Length)
            {
                throw new BackOffStrategyFailedException();
            }

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
                    var next = index + 1;

                    ExecuteBackOffStrategy(next);                    

                    break;
                }
            }
        }
    }
}
