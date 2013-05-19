using System;
using Jaevner.Core;

namespace Jaevner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 5)
            {
                Console.WriteLine("One or more arguments missing...");
                return;
            }

            var syncSettings = new SyncSettings();

            string path = args[0];
            syncSettings.CalendarUrl = args[1];
            syncSettings.UserName = args[2];
            syncSettings.Password = args[3];
            syncSettings.DaysToKeep = Convert.ToInt32(args[4]);

            var runner = new JaevnerRunner();
            runner.Run(path, syncSettings);

            Console.WriteLine("Finished!");
        }
    }
}
