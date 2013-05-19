namespace Jaevner.Core
{
    public class SyncSettings : ISyncSettings
    {
        public int DaysToKeep { get; set; }

        public string CalendarUrl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
