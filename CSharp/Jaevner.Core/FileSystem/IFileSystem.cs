﻿namespace Jaevner.Core
{
    public interface IFileSystem
    {
        bool FileExists(string path);
        string ReadAllText(string path);
    }
}
