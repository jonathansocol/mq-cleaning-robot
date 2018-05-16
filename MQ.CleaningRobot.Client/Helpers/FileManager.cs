using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MQ.CleaningRobot.Client.Helpers
{
    public static class FileManager
    {
        public static string LoadJsonFile(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        public static void ExportJsonFile(string json, string path)
        {
            File.WriteAllText(path, json);
        }
    }
}
