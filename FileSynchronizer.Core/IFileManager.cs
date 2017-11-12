using System;

namespace FileSynchronizer.Core
{
    public interface IFileManager
    {
        void Copy(string sourcePath, string destPath);

        void Delete(string path);
    }
}
