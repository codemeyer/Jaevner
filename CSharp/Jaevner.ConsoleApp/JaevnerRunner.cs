using System;
using System.Collections.Generic;
using System.IO;
using Jaevner.Core;

namespace Jaevner.ConsoleApp
{
    public class JaevnerRunner
    {
        public void Run(string path, SyncSettings syncSettings)
        {
            var service = GetService(syncSettings);

            service.EntryAction += ServiceOnEntryAction;

            ProcessFile(path, service);
        }

        private static JaevnerService GetService(SyncSettings syncSettings)
        {
            var repository = new CalendarRepository(syncSettings);
            var service = new JaevnerService(repository);

            return service;
        }

        private static void ProcessFile(string path, JaevnerService service)
        {
            string data = File.ReadAllText(path);
            var parser = new CsvParser();

            List<JaevnerEntry> entries = parser.Parse(data);

            Console.WriteLine("Found {0} entries in {1}", entries.Count, path);

            service.ProcessEntries(entries);
        }

        private void ServiceOnEntryAction(object sender, JaevnerEventArgs args)
        {
            string msg = string.Format("{0} entry {1} on {2}", args.Action, args.Entry.Title, args.Entry.StartDateTime.ToString("yyyy-MM-dd HH:mm"));
            Console.WriteLine(msg);
        }
    }
}
