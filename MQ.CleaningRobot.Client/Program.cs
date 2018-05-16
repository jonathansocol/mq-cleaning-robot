using MQ.CleaningRobot.Client.Helpers;
using MQ.CleaningRobot.Services;
using System;

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

            var cleaningService = new CleaningService();

            Console.WriteLine("Executing cleaning plan");

            var results = cleaningService.ExecuteCleaningProcess(robotInput);
            var jsonResults = JsonSerializer.Serialize(results);

            Console.WriteLine($"Exporting results to { resultFileName }");

            FileManager.ExportJsonFile(jsonResults, resultFileName);

            Console.WriteLine($"Results exported to { resultFileName }");
            Console.WriteLine("Press any key to close the window");

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
