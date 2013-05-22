using System;
using Jaevner.Core;
using Newtonsoft.Json;

namespace Jaevner.ConsoleApp
{
    public class SettingsParser
    {
        private const int DefaultDaysToKeep = 14;

        public SyncSettings GetSyncSettings(string[] args, string json)
        {
            SyncSettings settings = GetSettingsFromJson(json);
            CommandLineOptions cmdSettings = GetSettingsFromCommandLine(args);

            if (!string.IsNullOrWhiteSpace(cmdSettings.CalendarUrl))
            {
                settings.CalendarUrl = cmdSettings.CalendarUrl;
            }

            if (!string.IsNullOrWhiteSpace(cmdSettings.UserName))
            {
                settings.UserName = cmdSettings.UserName;
            }

            if (!string.IsNullOrWhiteSpace(cmdSettings.Password))
            {
                settings.Password = cmdSettings.Password;
            }

            if (cmdSettings.DaysToKeep != 0)
            {
                settings.DaysToKeep = cmdSettings.DaysToKeep;
            }
            else
            {
                if (settings.DaysToKeep == 0)
                {
                    settings.DaysToKeep = DefaultDaysToKeep;
                }
            }

            // TODO: throw exception if stuff is missing!

            return settings;
        }

        private SyncSettings GetSettingsFromJson(string json)
        {
            try
            {
                SyncSettings settings = JsonConvert.DeserializeObject<SyncSettings>(json);
                return settings;
            }
            catch (Exception)
            {
                return new SyncSettings();
            }
        }

        public string GetCalendarFile(string[] args)
        {
            CommandLineOptions options = GetSettingsFromCommandLine(args);

            if (args.Length == 1 && string.IsNullOrWhiteSpace(options.File))
            {
                return args[0];
            }
            else
            {
                return options.File;
            }
        }

        private CommandLineOptions GetSettingsFromCommandLine(string[] args)
        {
            var options = new CommandLineOptions();
            CommandLine.Parser.Default.ParseArguments(args, options);

            return options;
        }
    }
}
