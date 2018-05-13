using MQ.CleaningRobot.Helpers;
using MQ.CleaningRobot.Interfaces;
using System;
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
