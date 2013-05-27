namespace Jaevner.Core
{
    interface IFileSystem
    {
        bool FileExists(string path);
        string ReadAllText(string path);
    }
}
