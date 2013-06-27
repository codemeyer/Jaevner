namespace Jaevner.Core
{
    public interface ISyncSettings
    {
        string CalendarUrl { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}