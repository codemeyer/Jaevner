using System;
using Newtonsoft.Json;

namespace Jaevner.Core
{
    public class SettingsParser
    {
        private const int DefaultDaysToKeep = 14;

        public SyncSettings GetSyncSettings(string[] args, string json)
        {
            SyncSettings settings = GetSettingsFromJson(json);

            if (settings.DaysToKeep == 0)
            {
                if (args.Length > 1)
                {
                    int days;
                    bool success = int.TryParse(args[1], out days);

                    settings.DaysToKeep = success ? days : DefaultDaysToKeep;
                }
                else
                {
                    settings.DaysToKeep = DefaultDaysToKeep;
                }
            }

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
            if (args.Length > 0)
            {
                return args[0];
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
