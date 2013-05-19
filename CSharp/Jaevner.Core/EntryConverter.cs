using System.Linq;
using Google.GData.Calendar;
using Google.GData.Extensions;

namespace Jaevner.Core
{
    public class EntryConverter
    {
        public static JaevnerEntry GetJaevnerEntry(EventEntry eventEntry)
        {
            var entry = new JaevnerEntry();
            entry.Title = eventEntry.Title.Text;
            entry.Description = eventEntry.Content.Content;
            if (eventEntry.Locations.Count > 0)
            {
                entry.Location = eventEntry.Locations[0].ValueString;
            }
            else
            {
                entry.Location = string.Empty;
            }
            entry.StartDateTime = eventEntry.Times[0].StartTime;
            entry.EndDateTime = eventEntry.Times[0].EndTime;

            var property = eventEntry.ExtensionElements
                                     .OfType<ExtendedProperty>()
                                     .FirstOrDefault(e => e.Name.Equals("UniqueId"));

            if (property != default(ExtendedProperty))
            {
                entry.UniqueId = property.Value;
            }

            return entry;
        }

        public static EventEntry GetEventEntry(JaevnerEntry entry)
        {
            var eventEntry = new EventEntry(entry.Title, entry.Description, entry.Location);
            var eventTimes = new When(entry.StartDateTime, entry.EndDateTime);
            eventEntry.Times.Add(eventTimes);

            var extended = new ExtendedProperty();
            extended.Name = "UniqueId";
            extended.Value = entry.UniqueId;
            eventEntry.ExtensionElements.Add(extended);

            return eventEntry;
        }
    }
}