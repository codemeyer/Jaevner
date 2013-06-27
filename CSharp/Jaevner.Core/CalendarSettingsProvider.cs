using Newtonsoft.Json;

namespace Jaevner.Core
{
    public class CalendarSettingsProvider : ICalendarSettingsProvider
    {
        private readonly ISettingsFileReader _settingsFileReader;
        private readonly CalendarSettings _settings;

        public CalendarSettingsProvider(ISettingsFileReader settingsFileReader)
        {
            _settingsFileReader = settingsFileReader;
            _settings = GetSettings();
        }

        private CalendarSettings GetSettings()
        {
            try
            {
                string json = _settingsFileReader.ReadJson();

                CalendarSettings settings = JsonConvert.DeserializeObject<CalendarSettings>(json);
                return settings;
            }
            catch
            {
                return new CalendarSettings();
            }
        }

        public string CalendarUrl
        {
            get { return _settings.CalendarUrl; }
        }

        public string UserName
        {
            get { return _settings.UserName; }
        }

        public string Password
        {
            get { return _settings.Password; }
        }
    }
}