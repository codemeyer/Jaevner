using System;
using Jaevner.Core;
using StructureMap;

namespace Jaevner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupContainer();

            var runner = ObjectFactory.GetInstance<JaevnerRunner>();

            try
            {
                runner.Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception:");
                Console.WriteLine(ex.Message);
            }
        }

        private static void SetupContainer()
        {
            ObjectFactory.Initialize(x =>
                {
                    x.For<IFileSystem>().Use<FileSystem>();
                    x.For<ICalendarRepository>().Use<CalendarRepository>();
                    x.For<ICalendarSettingsProvider>().Use<CalendarSettingsProvider>();
                    x.For<ISettingsFileReader>().Use<SettingsFileReader>();
                });
        }
    }
}
