﻿using System.IO;
using System.Text;

namespace Jaevner.Core
{
    public class FileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path, Encoding.Default);
        }
    }
}
