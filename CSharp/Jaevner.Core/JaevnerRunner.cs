using System;
using System.Collections.Generic;
using System.IO;

namespace Jaevner.Core
{
    public class JaevnerRunner
    {
        public void Run(string[] args)
        {
            var settingsParser = new SettingsParser();

            string path = settingsParser.GetCalendarFile(args);

            var fileSystem = new FileSystem();

            if (!fileSystem.FileExists(path))
            {
                throw new FileNotFoundException("Cannot find specified calendar file", path);
            }

            string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string jsonFile = Path.Combine(exePath, "Settings.json");

            string json = "{}";
            if (fileSystem.FileExists(jsonFile))
            {
                json = File.ReadAllText(jsonFile);
            }

            SyncSettings syncSettings = settingsParser.GetSyncSettings(args, json);

            var service = GetService(syncSettings);

            service.EntryAction += ServiceOnEntryAction;
            service.EntryException += ServiceOnEntryException;

            ProcessFile(path, service, syncSettings.DaysToKeep);
            
            Console.WriteLine("Finished!");
        }

        private JaevnerService GetService(SyncSettings syncSettings)
        {
            var repository = new CalendarRepository(syncSettings);
            var service = new JaevnerService(repository);

            return service;
        }

        private void ProcessFile(string path, JaevnerService service, int daysToKeep)
        {
            var fileSystem = new FileSystem();
            string data = fileSystem.ReadAllText(path);
            var parser = new CsvParser();

            List<JaevnerEntry> entries = parser.Parse(data);

            Console.WriteLine("Found {0} entries in {1}", entries.Count, Path.GetFileName(path));

            service.ProcessEntries(entries);
            service.RemoveIrrelevantEntries(entries, daysToKeep);
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
