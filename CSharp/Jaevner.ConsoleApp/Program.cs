using System;

namespace Jaevner.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new JaevnerRunner();

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
    }
}
