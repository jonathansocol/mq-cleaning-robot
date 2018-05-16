using MQ.CleaningRobot.Client.Helpers;
using MQ.CleaningRobot.Dtos;
using MQ.CleaningRobot.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace MQ.CleaningRobot.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileName = args[0];
            var resultFileName = args[1];

            ValidateInputParameters(sourceFileName, resultFileName);

            Console.WriteLine($"Loading information from {sourceFileName}");

            var sourceFile = FileManager.LoadJsonFile(sourceFileName);

            var robotInput = JsonSerializer.Deserialize(sourceFile);

            var position = new RobotPosition(robotInput.Start.X, robotInput.Start.Y, robotInput.Start.Facing);
            var robot = new Robot(position, robotInput.Battery);

            var instructions = new CleaningPlanInstructionsDto
            {
                Map = robotInput.Map,
                Instructions = new Queue<string>(robotInput.Commands)
            };

            Console.WriteLine("Executing cleaning plan");

            var results = robot.ExecuteCleaningPlan(instructions);
            var jsonResults = JsonSerializer.Serialize(results);

            Console.WriteLine($"Exporting results to { resultFileName }");

            FileManager.ExportJsonFile(jsonResults, resultFileName);

            Console.WriteLine($"Results exported to { resultFileName }");

            Console.ReadLine();
        }

        private static void ValidateInputParameters(string sourceFileName, string resultFileName)
        {
            if (sourceFileName == null)
            {
                throw new ArgumentNullException($"The argument { nameof(sourceFileName) } be null");
            }

            if (resultFileName == null)
            {
                throw new ArgumentNullException($"The argument { nameof(resultFileName) } be null");
            }
        }         
    }
}
