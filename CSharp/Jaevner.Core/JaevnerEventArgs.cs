using System;

namespace Jaevner.Core
{
    public class JaevnerEventArgs : EventArgs
    {
        public JaevnerEntry Entry { get; set; }

        public EntryActionType Action { get; set; }
    }

    public enum EntryActionType
    {
        Inserted,
        Updated,
        Removed
    }
}
