using System;
using System.IO;
using Unity.Entities.Serialization;

namespace NyokoLogging
{
    public class LoggerNyoko
    {
        private static string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");

        public static void LogStringToFile(string logMessage)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(LogFilePath))
                {
                    sw.WriteLine($"{DateTime.Now} - {logMessage}");
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log($"Error writing to log file: {ex.Message}");
                // You may want to log this error in another way, print to console, etc.
            }
        }
    }
}
