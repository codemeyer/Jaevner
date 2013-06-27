using System;
using System.Collections.Generic;
using System.Linq;
using Google.GData.Calendar;

namespace Jaevner.Core
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly ISyncSettings _syncSettings;

        public CalendarRepository(ISyncSettings syncSettings)
        {
            _syncSettings = syncSettings;
        }

        public bool EntryExists(string uniqueId)
        {
            EventEntry entry = FindByUniqueId(uniqueId);

            return (entry != null);
        }

        private EventEntry FindByUniqueId(string uniqueId)
        {
            return FindByExtensionProperty("UniqueId", uniqueId);
        }

        private EventEntry FindByExtensionProperty(string name, string value)
        {
            CalendarService calendarService = GetCalendarService();

            var eventQuery = new EventQuery(_syncSettings.CalendarUrl);
            eventQuery.ExtraParameters = string.Format("extq=[{0}:{1}]", name, value);

            EventFeed eventFeed = calendarService.Query(eventQuery);

            var entries = eventFeed.Entries;
            if (entries.Count > 0)
            {
                var feedEntry = (EventEntry)entries.FirstOrDefault();

                return feedEntry;
            }

            return null;
        }

        public List<JaevnerEntry> ListEntries()
        {
            CalendarService calendarService = GetCalendarService();

            var eventQuery = new EventQuery(_syncSettings.CalendarUrl);

            EventFeed eventFeed = calendarService.Query(eventQuery);

            var jaevnerEntries = new List<JaevnerEntry>();

            var entries = eventFeed.Entries;
            if (entries.Count > 0)
            {
                foreach (object entry in entries)
                {
                    var feedEntry = (EventEntry)entry;

                    JaevnerEntry jaevnerEntry = EntryConverter.GetJaevnerEntry(feedEntry);
                    jaevnerEntries.Add(jaevnerEntry);
                }
            }

            return jaevnerEntries;
        }

        public void Insert(JaevnerEntry entry)
        {
            CalendarService calendarService = GetCalendarService();

            EventEntry eventEntry = EntryConverter.GetEventEntry(entry);

            var uri = new Uri(_syncSettings.CalendarUrl);
            calendarService.Insert(uri, eventEntry);
        }

        public void Update(JaevnerEntry entry)
        {
            CalendarService calendarService = GetCalendarService();

            EventEntry eventEntry = FindByUniqueId(entry.UniqueId);
            eventEntry.Title.Text = entry.Title;
            eventEntry.Times[0].StartTime = entry.StartDateTime;
            eventEntry.Times[0].EndTime = entry.EndDateTime;
            eventEntry.Times[0].AllDay = entry.AllDayEvent;
            eventEntry.Locations[0].ValueString = entry.Location;
            calendarService.Update(eventEntry);
        }

        public void Remove(JaevnerEntry entry)
        {
            CalendarService calendarService = GetCalendarService();

            if (entry.UniqueId != null)
            {
                EventEntry eventEntry = FindByUniqueId(entry.UniqueId);
                calendarService.Delete(eventEntry);
            }
        }

        private CalendarService GetCalendarService()
        {
            var calendarService = new CalendarService("JaevnerCalendar");
            calendarService.setUserCredentials(_syncSettings.UserName, _syncSettings.Password);

            return calendarService;
        }
    }
}
