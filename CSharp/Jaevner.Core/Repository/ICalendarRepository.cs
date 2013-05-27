using System.Collections.Generic;

namespace Jaevner.Core
{
    public interface ICalendarRepository
    {
        List<JaevnerEntry> ListEntries();
        void Insert(JaevnerEntry entry);
        void Remove(JaevnerEntry entry);
        bool EntryExists(string uniqueId);
        void Update(JaevnerEntry entry);
    }
}