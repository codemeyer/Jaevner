using System;
using System.Collections.Generic;
using System.IO;

namespace Jaevner.Core
{
    public class JaevnerRunner
    {
        private readonly IFileSystem _fileSystem;
        private readonly JaevnerService _service;

        public JaevnerRunner(IFileSystem fileSystem, JaevnerService service)
        {
            _fileSystem = fileSystem;
            _service = service;
        }

        public void Run(string[] args)
        {
            var settingsParser = new SettingsParser();

            string path = settingsParser.GetCalendarFile(args);

            if (!_fileSystem.FileExists(path))
            {
                throw new FileNotFoundException("Cannot find specified calendar file", path);
            }

            int daysToKeep = settingsParser.GetNumberOfDaysToKeep(args);

            _service.EntryAction += ServiceOnEntryAction;
            _service.EntryException += ServiceOnEntryException;

            List<JaevnerEntry> entries = ReadEntriesFromFile(path);
            Console.WriteLine("Found {0} entries in {1}", entries.Count, Path.GetFileName(path));

            _service.ProcessEntries(entries);
            _service.RemoveIrrelevantEntries(entries, daysToKeep);
            
            Console.WriteLine("Finished!");
        }

        private List<JaevnerEntry> ReadEntriesFromFile(string path)
        {
            var fileSystem = new FileSystem();
            string data = fileSystem.ReadAllText(path);
            
            var parser = new CsvParser();
            List<JaevnerEntry> entries = parser.Parse(data);

            return entries;
        }

        private void ServiceOnEntryAction(object sender, JaevnerEventArgs args)
        {
            string msg = string.Format("{0} entry {1} on {2}", args.Action, args.Entry.Title, args.Entry.StartDateTime.ToString("yyyy-MM-dd HH:mm"));
            Console.WriteLine(msg);
        }

        private void ServiceOnEntryException(object sender, JaevnerExceptionEventArgs args)
        {
            string msg = string.Format("Exception for {0} entry {1} on {2}: {3}", args.Action, args.Entry.Title, args.Entry.StartDateTime.ToString("yyyy-MM-dd HH:mm"), args.Exception);
            Console.WriteLine(msg);
        }
    }
}
