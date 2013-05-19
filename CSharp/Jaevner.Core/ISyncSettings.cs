namespace Jaevner.Core
{
    public interface ISyncSettings
    {
        int DaysToKeep { get; set; }
        string CalendarUrl { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}