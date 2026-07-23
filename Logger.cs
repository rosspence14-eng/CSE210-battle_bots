using System;
using System.IO;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        None - logging functionality only
    FUNCTION:
        Handles logging and recording of system events, messages, and debug information.
        Messages are written to the console and appended to a local log file.
    OUTPUTS:
        LogMessage(): void - no return value
    ---------------------------------------------*/
    public class Logger
    {
        private readonly string _npLogFilePath;

        public Logger()
        {
            _npLogFilePath = Path.Combine(AppContext.BaseDirectory, "battlebot.log");
        }

        public Logger(string logFilePath)
        {
            _npLogFilePath = logFilePath;
        }

        public void LogMessage(string message)
        {
            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            Console.WriteLine(entry);

            try
            {
                File.AppendAllText(_npLogFilePath, entry + Environment.NewLine);
            }
            catch (Exception)
            {
                // If file logging is unavailable, console output still works.
            }
        }
    }
}