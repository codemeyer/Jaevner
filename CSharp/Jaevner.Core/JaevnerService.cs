using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaevner.Core
{
    public class JaevnerService
    {
        private readonly ICalendarRepository _repository;

        public JaevnerService(ICalendarRepository repository)
        {
            _repository = repository;
        }

        public void RemoveIrrelevantEntries(List<JaevnerEntry> entries, int daysToKeep)
        {
            List<JaevnerEntry> existingEntries = _repository.ListEntries();

            foreach (JaevnerEntry existing in existingEntries)
            {
                TimeSpan howLongAgo = DateTime.Now.Subtract(existing.StartDateTime);
                if (howLongAgo.Days > daysToKeep)
                {
                    _repository.Remove(existing);
                    OnEntryAction(new JaevnerEventArgs { Entry = existing, Action = EntryActionType.Removed});
                }
                else
                {
                    if (!entries.Any(e => e.UniqueId.Equals(existing.UniqueId)))
                    {
                        _repository.Remove(existing);
                        OnEntryAction(new JaevnerEventArgs { Entry = existing, Action = EntryActionType.Removed });
                    }
                }
            }
        }

        public void ProcessEntries(List<JaevnerEntry> entries)
        {
            foreach (JaevnerEntry entry in entries)
            {
                if (_repository.EntryExists(entry.UniqueId))
                {
                    UpdateEntry(entry);
                }
                else
                {
                    InsertEntry(entry);
                }
            }
        }

        private void UpdateEntry(JaevnerEntry entry)
        {
            try
            {
                _repository.Update(entry);
                OnEntryAction(new JaevnerEventArgs {Entry = entry, Action = EntryActionType.Updated});
            }
            catch (Exception ex)
            {
                OnEntryException(new JaevnerExceptionEventArgs { Entry = entry, Action = EntryActionType.Inserted, Exception = ex });
            }
        }

        private void InsertEntry(JaevnerEntry entry)
        {
            try
            {
                _repository.Insert(entry);
                OnEntryAction(new JaevnerEventArgs { Entry = entry, Action = EntryActionType.Inserted });
            }
            catch (Exception ex)
            {
                OnEntryException(new JaevnerExceptionEventArgs { Entry = entry, Action = EntryActionType.Inserted, Exception = ex});
            }
        }


        public event EntryActionEventHandler EntryAction;

        private void OnEntryAction(JaevnerEventArgs args)
        {
            if (EntryAction != null)
            {
                EntryAction(this, args);
            }
        }

        public event EntryExceptionEventHandler EntryException;

        private void OnEntryException(JaevnerExceptionEventArgs args)
        {
            if (EntryException != null)
            {
                EntryException(this, args);
            }
        }
    }

    public delegate void EntryActionEventHandler(object sender, JaevnerEventArgs args);

    public delegate void EntryExceptionEventHandler(object sender, JaevnerExceptionEventArgs args);

}
