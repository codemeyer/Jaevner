using Newtonsoft.Json;

namespace Jaevner.Core
{
    public class SettingsParser
    {
        private const int DefaultDaysToKeep = 14;

        public SyncSettings GetSyncSettings(string[] args, string json)
        {
            SyncSettings settings = GetSettingsFromJson(json);

            if (args.Length > 1)
            {
                int days;
                bool success = int.TryParse(args[1], out days);

                settings.DaysToKeep = success ? days : DefaultDaysToKeep;
            }

            return settings;
        }

        private SyncSettings GetSettingsFromJson(string json)
        {
            SyncSettings settings = JsonConvert.DeserializeObject<SyncSettings>(json);
            return settings;
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
