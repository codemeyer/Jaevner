using System.IO;

namespace Jaevner.Core
{
    public class SettingsFileReader : ISettingsFileReader
    {
        private readonly IFileSystem _fileSystem;

        public SettingsFileReader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public string ReadJson()
        {
            string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string jsonFile = Path.Combine(exePath, "Settings.json");

            string json = "{}";
            if (_fileSystem.FileExists(jsonFile))
            {
                json = File.ReadAllText(jsonFile);
            }

            return json;
        }
    }
}