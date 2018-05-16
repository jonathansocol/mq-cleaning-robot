using MQ.CleaningRobot.Client.Helpers;
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

            if (sourceFileName == null || resultFileName == null)
            {
                throw new ArgumentNullException();
            }

            var sourceFile = LoadJson(sourceFileName);
            var jsonDeserializer = new JsonDeserializer();
            var robotInput = jsonDeserializer.Deserialize(sourceFile);

            var position = new Position(robotInput.Start.X, robotInput.Start.Y, robotInput.Start.Facing);

            var robot = new Robot(position, robotInput.Battery);

            robot.ExecuteCleaningPlan(robotInput.Map, new Queue<string>(robotInput.Commands));
        }

        public static string LoadJson(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
