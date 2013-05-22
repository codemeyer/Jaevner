using CommandLine;

namespace Jaevner.ConsoleApp
{
    public class CommandLineOptions
    {
        [Option("file", Required = true)]
        public string File { get; set; }

        [Option("calendarUrl", Required = false)]
        public string CalendarUrl { get; set; }

        [Option("user", Required = false)]
        public string UserName { get; set; }

        [Option("pwd", Required = false)]
        public string Password { get; set; }

        [Option("days", Required = false)]
        public int DaysToKeep { get; set; }
    }
}
