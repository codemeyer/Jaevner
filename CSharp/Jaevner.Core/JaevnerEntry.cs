using System;

namespace Jaevner.Core
{
    public class JaevnerEntry
    {
        public string Title { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public bool AllDayEvent { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string UniqueId { get; set; }
    }
}