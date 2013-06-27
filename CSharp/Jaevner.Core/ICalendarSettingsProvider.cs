namespace Jaevner.Core
{
    public interface ICalendarSettingsProvider
    {
        string CalendarUrl { get; }

        string UserName { get; }

        string Password { get; }
    }
}
